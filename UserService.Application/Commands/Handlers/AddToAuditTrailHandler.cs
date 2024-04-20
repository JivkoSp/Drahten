using UserService.Application.Exceptions;
using UserService.Application.Extensions;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class AddToAuditTrailHandler : ICommandHandler<AddToAuditTrailCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddToAuditTrailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddToAuditTrailCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var userTracking = new UserTracking(command.UserId, command.Action, command.DateTime.ToUtc(), command.Referrer);
                
            user.AddToAuditTrail(userTracking);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
