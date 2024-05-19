using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveTopicSubscriptionHandler : ICommandHandler<RemoveTopicSubscriptionCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveTopicSubscriptionHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(RemoveTopicSubscriptionCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            user.RemoveTopicSubscription(command.TopicId, command.UserId);
            
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
