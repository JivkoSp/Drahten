using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class BanUserHandler : ICommandHandler<BanUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;

        public BanUserHandler(IUserRepository userRepository, IUserReadService userReadService)
        {
            _userRepository = userRepository;
            _userReadService = userReadService;
        }

        public async Task HandleAsync(BanUserCommand command)
        {
            var issuer = await _userRepository.GetUserByIdAsync(command.IssuerUserId);

            if (issuer == null)
            {
                throw new UserNotFoundException(command.IssuerUserId);
            }

            var receiverExists = await _userReadService.ExistsByIdAsync(command.ReceiverUserId);

            if(receiverExists == false)
            {
                throw new UserNotFoundException(command.ReceiverUserId);
            }

            var banUser = new BannedUser(command.IssuerUserId, command.ReceiverUserId, command.DateTime);

            issuer.BanUser(banUser);

            await _userRepository.UpdateUserAsync(issuer);
        }
    }
}
