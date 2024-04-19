using NSubstitute;
using Shouldly;
using UserService.Application.Commands;
using UserService.Application.Commands.Handlers;
using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Entities;
using UserService.Domain.Factories;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;
using Xunit;

namespace UserService.Tests.Unit.Application.Handlers
{
    public class AddContactRequestHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly ICommandHandler<AddContactRequestCommand> _handler;

        private AddContactRequestCommand GetAddContactRequestCommand()
        {
            var command = new AddContactRequestCommand(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);

            return command;
        }

        public AddContactRequestHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _userReadService = Substitute.For<IUserReadService>();
            _handler = new AddContactRequestHandler(_userRepository, _userReadService);
        }

        #endregion

        private Task Act(AddContactRequestCommand command)
          => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the ReceiverUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ReceiverUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var addContactRequestCommand = GetAddContactRequestCommand();

            _userRepository.GetUserByIdAsync(addContactRequestCommand.ReceiverUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addContactRequestCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the read service that corresponds to the IssuerUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_IssuerUserId_Is_Not_Returned_From_ReadService()
        {
            //ARRANGE
            var addContactRequestCommand = GetAddContactRequestCommand();

            var user = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(addContactRequestCommand.ReceiverUserId).Returns(user);

            _userReadService.ExistsByIdAsync(addContactRequestCommand.IssuerUserId).Returns(false);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addContactRequestCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create ContactRequest value object if the IssuerUserId and ReceiverUserId from the AddContactRequestCommand
        //are valid Ids for existing User domain entities.
        //Additionally the created ContactRequest value object must be added to the User domain entity that corresponds to the receiver
        //side of the ContactRequest and the repository must be called to update that user entity.
        [Fact]
        public async Task Given_Valid_IssuerUserId_And_ReceiverUserId_Creates_And_Adds_ContactRequest_Instance_To_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var addContactRequestCommand = GetAddContactRequestCommand();

            var receiver = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(addContactRequestCommand.ReceiverUserId).Returns(receiver);

            _userReadService.ExistsByIdAsync(addContactRequestCommand.IssuerUserId).Returns(true);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addContactRequestCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(receiver);
        }
    }
}
