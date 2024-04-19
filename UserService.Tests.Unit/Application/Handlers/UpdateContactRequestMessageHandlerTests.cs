
using NSubstitute;
using Shouldly;
using UserService.Application.Commands;
using UserService.Application.Commands.Handlers;
using UserService.Application.Exceptions;
using UserService.Domain.Entities;
using UserService.Domain.Factories;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;
using Xunit;

namespace UserService.Tests.Unit.Application.Handlers
{
    public class UpdateContactRequestMessageHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<UpdateContactRequestMessageCommand> _handler;

        private UpdateContactRequestMessageCommand GetUpdateContactRequestMessageCommand(Guid? issuerUserId = null,
            Guid? receiverUserId = null)
        {
            var IssuerUserId = issuerUserId ?? Guid.NewGuid();

            var ReceiverUserId = receiverUserId ?? Guid.NewGuid();

            var command = new UpdateContactRequestMessageCommand(IssuerUserId, ReceiverUserId, "new message", DateTimeOffset.Now);

            return command;
        }

        private ContactRequest GetContactRequest()
        {
            var contactRequest = new ContactRequest(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);

            return contactRequest;
        }

        public UpdateContactRequestMessageHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new UpdateContactRequestMessageHandler(_userRepository);
        }

        #endregion

        private Task Act(UpdateContactRequestMessageCommand command) => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the IssuerUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_IssuerUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var updateContactRequestMessageCommand = GetUpdateContactRequestMessageCommand();

            _userRepository.GetUserByIdAsync(updateContactRequestMessageCommand.IssuerUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(updateContactRequestMessageCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the ReceiverUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ReceiverUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var updateContactRequestMessageCommand = GetUpdateContactRequestMessageCommand();

            var issuer = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(updateContactRequestMessageCommand.IssuerUserId).Returns(issuer);

            _userRepository.GetUserByIdAsync(updateContactRequestMessageCommand.ReceiverUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(updateContactRequestMessageCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should update the message of a ContactRequest value object that is issued by user that corresponds to the IssuerUserId
        //from the command if the IssuerUserId and ReceiverUserId from the command are valid Ids for existing User domain entities.
        //Additionally the repository must be called to update the user entity that corresponds to the IssuerUserId from the command.
        [Fact]
        public async Task Given_Valid_IssuerUserId_And_ReceiverUserId_Updates_Message_Of_ContactRequest_Instance_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var issuer = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            var receiver = _userConcreteFactory.Create(Guid.NewGuid(), "Doe John", "us3r", "doe@mail.com");

            var contactRequest = GetContactRequest();
            
            issuer.IssueContactRequest(contactRequest);

            var updateContactRequestMessageCommand = GetUpdateContactRequestMessageCommand(contactRequest.IssuerUserId, 
                contactRequest.ReceiverUserId);

            _userRepository.GetUserByIdAsync(updateContactRequestMessageCommand.IssuerUserId).Returns(issuer);

            _userRepository.GetUserByIdAsync(updateContactRequestMessageCommand.ReceiverUserId).Returns(receiver);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(updateContactRequestMessageCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(issuer);
        }
    }
}
