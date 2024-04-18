using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class AddContactRequestHandler : ICommandHandler<AddContactRequestCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IContactRequestFactory _contactRequestFactory;

        public AddContactRequestHandler(IUserRepository userRepository, IUserReadService userReadService, 
            IContactRequestFactory contactRequestFactory)
        {
            _userRepository = userRepository;
            _userReadService = userReadService;
            _contactRequestFactory = contactRequestFactory;
        }

        public async Task HandleAsync(AddContactRequestCommand command)
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

            var contactRequest = _contactRequestFactory.Create(command.IssuerUserId, command.ReceiverUserId, 
                command.DateTime, command.Message);

            receiver.AddContactRequest(contactRequest);

            await _userRepository.UpdateUserAsync(receiver);
        }
    }
}
