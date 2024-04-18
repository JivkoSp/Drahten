using UserService.Application.Exceptions;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class UpdateContactRequestMessageHandler : ICommandHandler<UpdateContactRequestMessageCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContactRequestFactory _contactRequestFactory;

        public UpdateContactRequestMessageHandler(IUserRepository userRepository, IContactRequestFactory contactRequestFactory)
        {
            _userRepository = userRepository;
            _contactRequestFactory = contactRequestFactory;
        }

        public async Task HandleAsync(UpdateContactRequestMessageCommand command)
        {
            var issuer = await _userRepository.GetUserByIdAsync(command.IssuerUserId);

            if (issuer == null)
            {
                throw new UserNotFoundException(command.IssuerUserId);
            }

            var contactRequest = _contactRequestFactory.Create(command.IssuerUserId,
                command.ReceiverUserId, command.DateTime, command.Message);

            issuer.RemoveIssuedContactRequest(command.ReceiverUserId);

            issuer.AddContactRequest(contactRequest);

            await _userRepository.UpdateUserAsync(issuer);
        }
    }
}
