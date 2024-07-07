using NSubstitute;
using PublicHistoryService.Application.Commands;
using PublicHistoryService.Application.Commands.Handlers;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.Factories;
using PublicHistoryService.Domain.Factories.Interfaces;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PublicHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class RemoveViewedUserHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly IViewedUserReadService _viewedUserReadService;
        private readonly ICommandHandler<RemoveViewedUserCommand> _handler;

        private RemoveViewedUserCommand GetRemoveViewedUserCommand()
        {
            var command = new RemoveViewedUserCommand(ViewerUserId: Guid.NewGuid(), ViewedUserId: Guid.NewGuid());

            return command;
        }

        private ViewedUser GetViewedUser()
        {
            var viewedUser = new ViewedUser(viewerUserID: Guid.NewGuid(), viewedUserID: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return viewedUser;
        }

        private ViewedUserDto GetViewedUserDto(ViewedUser viewedUser)
        {
            var viewedUserDto = new ViewedUserDto
            {
                ViewedUserReadModelId = Guid.NewGuid(),
                ViewerUserId = viewedUser.ViewerUserId.Value.ToString(),
                ViewedUserId = viewedUser.ViewedUserId.Value.ToString(),
                DateTime = viewedUser.DateTime
            };

            return viewedUserDto;
        }

        public RemoveViewedUserHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _viewedUserReadService = Substitute.For<IViewedUserReadService>();
            _handler = new RemoveViewedUserHandler(_userRepository, _viewedUserReadService);
        }

        #endregion

        private Task Act(RemoveViewedUserCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the ViewerUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ViewerUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeViewedUserCommand = GetRemoveViewedUserCommand();

            _userRepository.GetUserByIdAsync(removeViewedUserCommand.ViewerUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should throw ViewedUserNotFoundException when the following condition is met:
        //There is no ViewedUser returned from the repository that corresponds to the ViewedUserId from the command.
        [Fact]
        public async Task Throws_ViewedUserNotFoundException_When_User_With_Given_ViewedUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var removeViewedUserCommand = GetRemoveViewedUserCommand();

            _userRepository.GetUserByIdAsync(removeViewedUserCommand.ViewerUserId).Returns(user);

            _viewedUserReadService.GetViewedUserByIdAsync(removeViewedUserCommand.ViewedUserId)
                .Returns(default(ViewedUserDto));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ViewedUserNotFoundException>();
        }

        //Should remove ViewedUser value object if the ViewerUserId and ViewedUserId from the RemoveViewedUserCommand are valid Ids.
        //The repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_ViewerUserId_And_ViewedUserId_Removes_ViewedUser_Instance_From_Viewer_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var viewedUserValueObject = GetViewedUser();

            var viewedUserDto = GetViewedUserDto(viewedUserValueObject);

            var removeViewedUserCommand = GetRemoveViewedUserCommand();

            user.AddViewedUser(viewedUserValueObject);

            _userRepository.GetUserByIdAsync(removeViewedUserCommand.ViewerUserId).Returns(user);

            _viewedUserReadService.GetViewedUserByIdAsync(removeViewedUserCommand.ViewedUserId)
                .Returns(viewedUserDto);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedUserCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
