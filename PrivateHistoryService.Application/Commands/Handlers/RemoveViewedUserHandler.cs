using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveViewedUserHandler : ICommandHandler<RemoveViewedUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IViewedUserReadService _viewedUserReadService;

        public RemoveViewedUserHandler(IUserRepository userRepository, IViewedUserReadService viewedUserReadService)
        {
            _userRepository = userRepository;
            _viewedUserReadService = viewedUserReadService;
        }

        public async Task HandleAsync(RemoveViewedUserCommand command)
        {
            var viewer = await _userRepository.GetUserByIdAsync(command.ViewerUserId);

            if (viewer == null)
            {
                throw new UserNotFoundException(command.ViewerUserId);
            }
            
            var viewedUserDto = await _viewedUserReadService.GetViewedUserByIdAsync(command.ViewedUserId);

            if (viewedUserDto == null)
            {
                throw new ViewedUserNotFoundException(command.ViewedUserId);
            }

            var viewedUserValueObject = new ViewedUser(Guid.Parse(viewedUserDto.ViewerUserId),
                Guid.Parse(viewedUserDto.ViewedUserId), viewedUserDto.DateTime.ToUtc());

            viewer.RemoveViewedUser(viewedUserValueObject);

            await _userRepository.UpdateUserAsync(viewer);
        }
    }
}
