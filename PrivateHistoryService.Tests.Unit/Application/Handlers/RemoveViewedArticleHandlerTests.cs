using NSubstitute;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PrivateHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class RemoveViewedArticleHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<RemoveViewedArticleCommand> _handler;

        private RemoveViewedArticleCommand GetRemoveViewedArticleCommand(Guid? articleId = null, Guid? userId = null, DateTimeOffset? dateTime = null)
        {
            var ArticleId = articleId ?? Guid.NewGuid();
            var UserId = userId ?? Guid.NewGuid();
            var DateTime = dateTime ?? DateTimeOffset.Now;

            var command = new RemoveViewedArticleCommand(ArticleId: ArticleId, UserId: UserId, DateTime: DateTime);

            return command;
        }

        private ViewedArticle GetViewedArticle()
        {
            var viewedArticle = new ViewedArticle(articleId: Guid.NewGuid(), userId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return viewedArticle;
        }

        public RemoveViewedArticleHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new RemoveViewedArticleHandler(_userRepository);
        }

        #endregion

        private Task Act(RemoveViewedArticleCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeViewedArticleCommand = GetRemoveViewedArticleCommand();

            _userRepository.GetUserByIdAsync(removeViewedArticleCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedArticleCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should remove ViewedArticle value object if the UserId from the RemoveViewedArticleCommand is valid Id for an existing User.
        //The ViewedArticle value object to be removed must have the same values as those in the RemoveViewedArticleCommand.
        //Ensure the repository is called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_Removes_ViewedArticle_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var viewedArticle = GetViewedArticle();

            var removeViewedArticleCommand = GetRemoveViewedArticleCommand(viewedArticle.ArticleID, viewedArticle.UserID, viewedArticle.DateTime);

            user.AddViewedArticle(viewedArticle);

            _userRepository.GetUserByIdAsync(removeViewedArticleCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedArticleCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
