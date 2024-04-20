using UserService.Application.Exceptions;
using UserService.Application.Extensions;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class AddContactRequestHandler : ICommandHandler<AddContactRequestCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddContactRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddContactRequestCommand command)
        {
            var receiver = await _userRepository.GetUserByIdAsync(command.ReceiverUserId);

            if (receiver == null)
            {
                throw new UserNotFoundException(command.ReceiverUserId);
            }

            var issuer = await _userRepository.GetUserByIdAsync(command.IssuerUserId);

            if (issuer == null)
            {
                throw new UserNotFoundException(command.IssuerUserId);
            }

            var contactRequest = new ContactRequest(command.IssuerUserId, command.ReceiverUserId,
                command.DateTime.ToUtc(), command.Message);

            issuer.IssueContactRequest(contactRequest);

            receiver.ReceiveContactRequest(contactRequest);

            await _userRepository.UpdateUserAsync(receiver);
        }
    }
}
