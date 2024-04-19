using UserService.Application.Commands.Handlers;
using UserService.Application.Commands;
using UserService.Domain.Repositories;
using UserService.Application.Services.ReadServices;
using NSubstitute;
using Xunit;
using UserService.Domain.Entities;
using Shouldly;
using UserService.Application.Exceptions;
using UserService.Domain.Factories;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;

namespace UserService.Tests.Unit.Application.Handlers
{
    public class RemoveIssuedContactRequestHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly ICommandHandler<RemoveIssuedContactRequestCommand> _handler;

        private RemoveIssuedContactRequestCommand GetRemoveIssuedContactRequestCommand(Guid? issuerUserId = null, 
            Guid? receiverUserId = null)
        {
            var IssuerUserId = issuerUserId ?? Guid.NewGuid();

            var ReceiverUserId = receiverUserId ?? Guid.NewGuid();

            var command = new RemoveIssuedContactRequestCommand(IssuerUserId, ReceiverUserId);

            return command;
        }

        private ContactRequest GetContactRequest()
        {
            var contactRequest = new ContactRequest(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);

            return contactRequest;
        }

        public RemoveIssuedContactRequestHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _userReadService = Substitute.For<IUserReadService>();
            _handler = new RemoveIssuedContactRequestHandler(_userRepository, _userReadService);
        }

        #endregion

        private Task Act(RemoveIssuedContactRequestCommand command)
        => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the IssuerUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_IssuerUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeIssuedContactRequestCommand = GetRemoveIssuedContactRequestCommand();

            _userRepository.GetUserByIdAsync(removeIssuedContactRequestCommand.IssuerUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeIssuedContactRequestCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the read service that corresponds to the ReceiverUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ReceiverUserId_Is_Not_Returned_From_ReadService()
        {
            //ARRANGE
            var removeIssuedContactRequestCommand = GetRemoveIssuedContactRequestCommand();

            var issuer = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(removeIssuedContactRequestCommand.IssuerUserId).Returns(issuer);

            _userReadService.ExistsByIdAsync(removeIssuedContactRequestCommand.ReceiverUserId).Returns(false);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeIssuedContactRequestCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should remove ContactRequest value object that is issued by user that corresponds to the IssuerUserId from the BanUserCommand
        //if the IssuerUserId and ReceiverUserId from the BanUserCommand are valid Ids for existing User domain entities.
        //Additionally the the repository must be called to update the user entity that corresponds to the
        //IssuerUserId from the BanUserCommand.
        [Fact]
        public async Task Given_Valid_IssuerUserId_And_ReceiverUserId_Removes_ContactRequest_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var issuer = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            var contactRequest = GetContactRequest();

            var removeIssuedContactRequestCommand = GetRemoveIssuedContactRequestCommand(contactRequest.IssuerUserId, 
                contactRequest.ReceiverUserId);

            issuer.IssueContactRequest(contactRequest);

            _userRepository.GetUserByIdAsync(removeIssuedContactRequestCommand.IssuerUserId).Returns(issuer);

            _userReadService.ExistsByIdAsync(removeIssuedContactRequestCommand.ReceiverUserId).Returns(true);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeIssuedContactRequestCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(issuer);
        }
    }
}
