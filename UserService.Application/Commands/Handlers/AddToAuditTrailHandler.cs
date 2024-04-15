using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class AddToAuditTrailHandler : ICommandHandler<AddToAuditTrailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IUserTrackingFactory _userTrackingFactory;

        public AddToAuditTrailHandler(IUserRepository userRepository, IUserReadService userReadService,
            IUserTrackingFactory userTrackingFactory)
        {
            _userRepository = userRepository;
            _userReadService = userReadService;
            _userTrackingFactory = userTrackingFactory;
        }

        public async Task HandleAsync(AddToAuditTrailCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var userTracking = _userTrackingFactory.Create(command.UserId, command.Action, command.DateTime, command.Referrer);

            user.AddToAuditTrail(userTracking);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
