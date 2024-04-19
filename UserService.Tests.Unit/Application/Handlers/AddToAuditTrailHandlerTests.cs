using UserService.Application.Commands.Handlers;
using UserService.Application.Commands;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;
using UserService.Domain.Factories;
using NSubstitute;
using Xunit;
using UserService.Domain.Entities;
using Shouldly;
using UserService.Application.Exceptions;

namespace UserService.Tests.Unit.Application.Handlers
{
    public class AddToAuditTrailHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<AddToAuditTrailCommand> _handler;

        private AddToAuditTrailCommand GetAddToAuditTrailCommand()
        {
            var command = new AddToAuditTrailCommand(Guid.NewGuid(), "SignedIn", DateTimeOffset.Now, "https://example.com");

            return command;
        }

        public AddToAuditTrailHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new AddToAuditTrailHandler(_userRepository);
        }

        #endregion

        private Task Act(AddToAuditTrailCommand command)
          => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var addToAuditTrailCommand = GetAddToAuditTrailCommand();

            _userRepository.GetUserByIdAsync(addToAuditTrailCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addToAuditTrailCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create UserTracking value object if the UserId from the AddContactRequestCommand
        //is valid Id for existing User domain entity.
        //Additionally the created UserTracking value object must be added to the User domain entity that corresponds to the UserId
        //from the command and the repository must be called to update that user entity.
        [Fact]
        public async Task Given_Valid_UserId_Creates_And_Adds_UserTracking_Instance_To_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var addToAuditTrailCommand = GetAddToAuditTrailCommand();

            var user = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(addToAuditTrailCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addToAuditTrailCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
