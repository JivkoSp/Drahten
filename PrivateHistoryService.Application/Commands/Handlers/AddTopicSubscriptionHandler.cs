using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class AddTopicSubscriptionHandler : ICommandHandler<AddTopicSubscriptionCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddTopicSubscriptionHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddTopicSubscriptionCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var topicSubscription = new TopicSubscription(command.TopicId, command.UserId, command.DateTime.ToUtc());

            user.AddTopicSubscription(topicSubscription);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
