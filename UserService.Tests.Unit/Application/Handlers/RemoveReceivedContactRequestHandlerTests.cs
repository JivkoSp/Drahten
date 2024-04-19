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
    public class RemoveReceivedContactRequestHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly ICommandHandler<RemoveReceivedContactRequestCommand> _handler;

        private RemoveReceivedContactRequestCommand GetRemoveReceivedContactRequestCommand(Guid? issuerUserId = null,
            Guid? receiverUserId = null)
        {
            var IssuerUserId = issuerUserId ?? Guid.NewGuid();

            var ReceiverUserId = receiverUserId ?? Guid.NewGuid();

            var command = new RemoveReceivedContactRequestCommand(ReceiverUserId, IssuerUserId);

            return command;
        }

        private ContactRequest GetContactRequest()
        {
            var contactRequest = new ContactRequest(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);

            return contactRequest;
        }

        public RemoveReceivedContactRequestHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _userReadService = Substitute.For<IUserReadService>();
            _handler = new RemoveReceivedContactRequestHandler(_userRepository, _userReadService);
        }

        #endregion

        private Task Act(RemoveReceivedContactRequestCommand command)
        => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the ReceiverUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ReceiverUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeReceivedContactRequestCommand = GetRemoveReceivedContactRequestCommand();

            _userRepository.GetUserByIdAsync(removeReceivedContactRequestCommand.ReceiverUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeReceivedContactRequestCommand));

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
            var removeReceivedContactRequestCommand = GetRemoveReceivedContactRequestCommand();

            var receiver = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(removeReceivedContactRequestCommand.ReceiverUserId).Returns(receiver);

            _userReadService.ExistsByIdAsync(removeReceivedContactRequestCommand.IssuerUserId).Returns(false);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeReceivedContactRequestCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should remove ContactRequest value object that is received by user that corresponds to the ReceiverUserId from the command
        //if the IssuerUserId and ReceiverUserId from the command are valid Ids for existing User domain entities.
        //Additionally the repository must be called to update the user entity that corresponds to the
        //ReceiverUserId from the command.
        [Fact]
        public async Task Given_Valid_IssuerUserId_And_ReceiverUserId_Removes_ContactRequest_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var receiver = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            var contactRequest = GetContactRequest();

            var removeReceivedContactRequestCommand = GetRemoveReceivedContactRequestCommand(contactRequest.IssuerUserId, 
                contactRequest.ReceiverUserId);

            receiver.ReceiveContactRequest(contactRequest);

            _userRepository.GetUserByIdAsync(removeReceivedContactRequestCommand.ReceiverUserId).Returns(receiver);

            _userReadService.ExistsByIdAsync(removeReceivedContactRequestCommand.IssuerUserId).Returns(true);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeReceivedContactRequestCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(receiver);
        }
    }
}
