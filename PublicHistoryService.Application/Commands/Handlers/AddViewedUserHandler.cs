using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Application.Commands.Handlers
{
    internal sealed class AddViewedUserHandler : ICommandHandler<AddViewedUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddViewedUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddViewedUserCommand command)
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

            var viewedUserValueObject = new ViewedUser(command.ViewerUserId, command.ViewedUserId, command.DateTime.ToUniversalTime());

            viewer.AddViewedUser(viewedUserValueObject);

            await _userRepository.UpdateUserAsync(viewer);
        }
    }
}
