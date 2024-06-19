using UserService.Application.AsyncDataServices;
using UserService.Application.Dtos;
using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly IUserReadService _userReadService;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public CreateUserHandler(IUserRepository userRepository, IUserFactory userFactory, 
            IUserReadService userReadService, IMessageBusPublisher messageBusPublisher)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
            _userReadService = userReadService;
            _messageBusPublisher = messageBusPublisher;
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(command.UserId);

            if (alreadyExists)
            {
                throw new UserAlreadyExistsException(command.UserId);
            }

            var user = _userFactory.Create(command.UserId, command.UserFullName, command.UserNickName, command.UserEmailAddress);

            await _userRepository.AddUserAsync(user);

            //Send Async message to a message bus.

            //var userPublishedDto = new UserPublishedDto
            //{
            //    UserId = command.UserId,
            //    Event = "User_Published"
            //};

            //_messageBusPublisher.PublishNewUser(userPublishedDto);  
        }
    }
}
