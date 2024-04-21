using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class RegisterUserTopicHandler : ICommandHandler<RegisterUserTopicCommand>
    {
        private readonly ITopicReadService _topicReadService;
        private readonly IUserRepository _userRepository;

        public RegisterUserTopicHandler(ITopicReadService topicReadService, IUserRepository userRepository)
        {
            _topicReadService = topicReadService;
            _userRepository = userRepository;
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

            var userTopic = new UserTopic(command.UserId, command.TopicId, command.DateTime.ToUtc());

            user.SubscribeToTopic(userTopic);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
