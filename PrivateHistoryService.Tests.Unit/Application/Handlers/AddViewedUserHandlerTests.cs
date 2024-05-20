using NSubstitute;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;
using Shouldly;
using Xunit;

namespace PrivateHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class AddViewedUserHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<AddViewedUserCommand> _handler;

        private AddViewedUserCommand GetAddViewedUserCommand(Guid? userId = null)
        {
            var UserId = userId ?? Guid.NewGuid();

            var command = new AddViewedUserCommand(ViewerUserId: Guid.NewGuid(), ViewedUserId: UserId, DateTime: DateTimeOffset.Now);

            return command;
        }

        public AddViewedUserHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new AddViewedUserHandler(_userRepository);
        }

        #endregion

        private Task Act(AddViewedUserCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the ViewerUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ViewerUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var addViewedUserCommand = GetAddViewedUserCommand();

            _userRepository.GetUserByIdAsync(addViewedUserCommand.ViewerUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addViewedUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the ViewedUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ViewedUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var addViewedUserCommand = GetAddViewedUserCommand();

            _userRepository.GetUserByIdAsync(addViewedUserCommand.ViewerUserId).Returns(user);

            _userRepository.GetUserByIdAsync(addViewedUserCommand.ViewedUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addViewedUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create ViewedUser value object if the ViewerUserId and the ViewedUserId from the AddViewedUserCommand
        //are valid Id's for existing User's.
        //Additionally the created ViewedUser value object must be added to the User that is corresponding to the ViewerUserId and the 
        //repository must be called to update that User.
        [Fact]
        public async Task Given_Valid_ViewerUserId_And_ViewedUserId_Creates_And_Adds_ViewedUser_Instance_To_Viewer_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var viewerUser = _userConcreteFactory.Create(Guid.NewGuid());

            var viewedUser = _userConcreteFactory.Create(Guid.NewGuid());

            var addViewedUserCommand = GetAddViewedUserCommand();

            _userRepository.GetUserByIdAsync(addViewedUserCommand.ViewerUserId).Returns(viewerUser);

            _userRepository.GetUserByIdAsync(addViewedUserCommand.ViewedUserId).Returns(viewedUser);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addViewedUserCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(viewerUser);
        }
    }
}
