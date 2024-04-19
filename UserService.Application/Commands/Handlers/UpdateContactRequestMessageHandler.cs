using UserService.Application.Exceptions;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class UpdateContactRequestMessageHandler : ICommandHandler<UpdateContactRequestMessageCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateContactRequestMessageHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UpdateContactRequestMessageCommand command)
        {
            var issuer = await _userRepository.GetUserByIdAsync(command.IssuerUserId);

            if (issuer == null)
            {
                throw new UserNotFoundException(command.IssuerUserId);
            }

            var receiver = await _userRepository.GetUserByIdAsync(command.ReceiverUserId);

            if (receiver == null)
            {
                throw new UserNotFoundException(command.ReceiverUserId);
            }

            var contactRequest = new ContactRequest(command.IssuerUserId, command.ReceiverUserId, command.DateTime, command.Message);
                
            issuer.RemoveIssuedContactRequest(command.ReceiverUserId);

            issuer.IssueContactRequest(contactRequest);

            receiver.ReceiveContactRequest(contactRequest);

            await _userRepository.UpdateUserAsync(issuer);
        }
    }
}
