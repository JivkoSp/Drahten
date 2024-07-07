using NSubstitute;
using PublicHistoryService.Application.Commands;
using PublicHistoryService.Application.Commands.Handlers;
using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.Factories;
using PublicHistoryService.Domain.Factories.Interfaces;
using PublicHistoryService.Domain.Repositories;
using Shouldly;
using Xunit;

namespace PublicHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class AddViewedArticleHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<AddViewedArticleCommand> _handler;

        private AddViewedArticleCommand GetAddViewedArticleCommand(Guid? userId = null)
        {
            var UserId = userId ?? Guid.NewGuid();

            var command = new AddViewedArticleCommand(ArticleId: Guid.NewGuid(), UserId: UserId, DateTime: DateTimeOffset.Now);

            return command;
        }

        public AddViewedArticleHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new AddViewedArticleHandler(_userRepository);
        }

        #endregion

        private Task Act(AddViewedArticleCommand command)
            => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var addViewedArticleCommand = GetAddViewedArticleCommand();

            _userRepository.GetUserByIdAsync(addViewedArticleCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addViewedArticleCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create ViewedArticle value object if the UserId from the AddViewedArticleCommand is valid Id for existing User.
        //Additionally the created ViewedArticle value object must be added to the User and the repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_Creates_And_Adds_ViewedArticle_Instance_To_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var addViewedArticleCommand = GetAddViewedArticleCommand(user.Id);

            _userRepository.GetUserByIdAsync(addViewedArticleCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addViewedArticleCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
