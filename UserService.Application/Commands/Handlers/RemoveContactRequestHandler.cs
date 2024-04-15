using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class RemoveContactRequestHandler : ICommandHandler<RemoveContactRequestCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;

        public RemoveContactRequestHandler(IUserRepository userRepository, IUserReadService userReadService)
        {
            _userRepository = userRepository;
            _userReadService = userReadService;
        }

        public async Task HandleAsync(RemoveContactRequestCommand command)
        {
            var receiver = await _userRepository.GetUserByIdAsync(command.ReceiverUserId);

            if (receiver == null)
            {
                throw new UserNotFoundException(command.ReceiverUserId);
            }

            var issuerExists = await _userReadService.ExistsByIdAsync(command.IssuerUserId);

            if (issuerExists == false)
            {
                throw new UserNotFoundException(command.IssuerUserId);
            }

            receiver.RemoveContactRequest(command.IssuerUserId);

            await _userRepository.UpdateUserAsync(receiver);
        }
    }
}
