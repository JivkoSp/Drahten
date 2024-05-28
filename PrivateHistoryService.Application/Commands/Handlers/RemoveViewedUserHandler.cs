using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveViewedUserHandler : ICommandHandler<RemoveViewedUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveViewedUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(RemoveViewedUserCommand command)
        {
            var viewer = await _userRepository.GetUserByIdAsync(command.ViewerUserId);

            if (viewer == null)
            {
                throw new UserNotFoundException(command.ViewerUserId);
            }

            var viewedUser = await _userRepository.GetUserByIdAsync(command.ViewedUserId);

            if (viewedUser == null)
            {
                throw new UserNotFoundException(command.ViewedUserId);
            }

            var viewedUserValueObject = new ViewedUser(command.ViewerUserId, command.ViewedUserId, command.DateTime.ToUtc());

            viewer.RemoveViewedUser(viewedUserValueObject);

            await _userRepository.UpdateUserAsync(viewer);
        }
    }
}
