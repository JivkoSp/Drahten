using NSubstitute;
using Shouldly;
using UserService.Application.AsyncDataServices;
using UserService.Application.Commands;
using UserService.Application.Commands.Handlers;
using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Factories;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;
using Xunit;

namespace UserService.Tests.Unit.Application.Handlers
{
    public class CreateUserHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserFactory _userMockFactory;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly ICommandHandler<CreateUserCommand> _handler;

        private CreateUserCommand GetCreateUserCommand()
        {
            var command = new CreateUserCommand(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            return command;
        }

        public CreateUserHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userMockFactory = Substitute.For<IUserFactory>();
            _userRepository = Substitute.For<IUserRepository>();
            _userReadService = Substitute.For<IUserReadService>();
            _messageBusPublisher = Substitute.For<IMessageBusPublisher>();
            _handler = new CreateUserHandler(_userRepository, _userMockFactory, _userReadService, _messageBusPublisher);
        }

        #endregion

        private Task Act(CreateUserCommand command)
         => _handler.HandleAsync(command);

        //Should throw UserAlreadyExistsException when the following condition is met:
        //There is already User domain entity with the same UserId as the UserId from the CreateUserCommand.
        [Fact]
        public async Task DuplicateUser_Throws_UserAlreadyExistsException()
        {
            //ARRANGE
            var createUserCommand = GetCreateUserCommand();

            _userReadService.ExistsByIdAsync(createUserCommand.UserId).Returns(true);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(createUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserAlreadyExistsException>();
        }

        //Should create new domain User entity if there is NOT already User domain entity with the same
        //UserId as the UserId from the CreateUserCommand (e.g If the UserId from the CreateUserCommand is valid).
        //--------------------------------------------------------------------------
        //If the User entity is created successfully the repository must also be called one time.
        [Fact]
        public async Task GivenValidUserId_Calls_Repository_On_Success()
        {
            //ARRANGE
            var createUserCommand = GetCreateUserCommand();

            var user = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userReadService.ExistsByIdAsync(createUserCommand.UserId).Returns(false);

            //ACT
            _userMockFactory.Create(createUserCommand.UserId, createUserCommand.UserFullName, createUserCommand.UserNickName, 
                createUserCommand.UserEmailAddress).Returns(user);

            var exception = await Record.ExceptionAsync(async () => await Act(createUserCommand));

            //ASSERT
            exception.ShouldBeNull();

            _userMockFactory.Received(1).Create(createUserCommand.UserId, createUserCommand.UserFullName, createUserCommand.UserNickName,
                createUserCommand.UserEmailAddress);

            await _userRepository.Received(1).AddUserAsync(user);
        }
    }
}
