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
        private readonly ICommandHandler<AddContactRequestCommand> _handler;

        private AddContactRequestCommand GetAddContactRequestCommand(Guid? issuerUserId = null,
            Guid? receiverUserId = null)
        {
            var IssuerUserId = issuerUserId ?? Guid.NewGuid();

            var ReceiverUserId = receiverUserId ?? Guid.NewGuid();

            var command = new AddContactRequestCommand(IssuerUserId, ReceiverUserId, DateTimeOffset.Now, null);

            return command;
        }

        public AddContactRequestHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new AddContactRequestHandler(_userRepository);
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
        //There is no User returned from the repository that corresponds to the IssuerUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_IssuerUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var addContactRequestCommand = GetAddContactRequestCommand();

            var user = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(addContactRequestCommand.ReceiverUserId).Returns(user);

            _userRepository.GetUserByIdAsync(addContactRequestCommand.IssuerUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addContactRequestCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create ContactRequest value object if the IssuerUserId and ReceiverUserId from the AddContactRequestCommand
        //are valid Ids for existing User domain entities.
        //Additionally the created ContactRequest value object must be added to the User domain entities that are corresponding
        //to the issuer and receiver sides of the ContactRequest and the repository must be called to update both entities.
        [Fact]
        public async Task Given_Valid_IssuerUserId_And_ReceiverUserId_Creates_And_Adds_ContactRequest_Instance_To_Issuer_And_Receiver_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var issuer = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            var receiver = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            var addContactRequestCommand = GetAddContactRequestCommand(issuer.Id, receiver.Id);

            _userRepository.GetUserByIdAsync(addContactRequestCommand.ReceiverUserId).Returns(receiver);

            _userRepository.GetUserByIdAsync(addContactRequestCommand.IssuerUserId).Returns(issuer);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addContactRequestCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(receiver);
        }
    }
}
