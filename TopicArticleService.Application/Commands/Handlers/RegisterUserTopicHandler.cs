using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class RegisterUserTopicHandler : ICommandHandler<RegisterUserTopicCommand>
    {
        private readonly ITopicReadService _topicReadService;
        private readonly IUserRepository _userRepository;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public RegisterUserTopicHandler(ITopicReadService topicReadService, IUserRepository userRepository, IMessageBusPublisher messageBusPublisher)
        {
            _topicReadService = topicReadService;
            _userRepository = userRepository;
            _messageBusPublisher = messageBusPublisher;
        }

        public async Task HandleAsync(RegisterUserTopicCommand command)
        {
            var topicAlreadyExists = await _topicReadService.ExistsByIdAsync(command.TopicId);

            if(topicAlreadyExists == false)
            {
                throw new TopicNotFoundException(command.TopicId);
            }

            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if(user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var topicSubscriptionDto = new TopicSubscriptionDto
            {
                TopicId = command.TopicId,
                UserId = command.UserId.ToString(),
                DateTime = command.DateTime,
                Event = "TopicSubscription"
            };

            //Post message to the message broker about adding topic subscription for topic with ID: TopicId by user with ID: UserId.
            await _messageBusPublisher.PublishTopicSubscriptionAsync(topicSubscriptionDto);

            var userTopic = new UserTopic(command.UserId, command.TopicId, command.DateTime.ToUniversalTime());

            user.SubscribeToTopic(userTopic);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
