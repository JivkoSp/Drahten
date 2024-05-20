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
    public sealed class RemoveCommentedArticleHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<RemoveCommentedArticleCommand> _handler;

        private RemoveCommentedArticleCommand GetRemoveCommentedArticleCommand(Guid? articleId = null, Guid? userId = null,
            string articleComment = null, DateTimeOffset? dateTime = null)
        {
            var ArticleId = articleId ?? Guid.NewGuid();
            var UserId = userId ?? Guid.NewGuid();
            var ArticleComment = articleComment ?? "...";
            var DateTime = dateTime ?? DateTimeOffset.Now;

            var command = new RemoveCommentedArticleCommand(ArticleId: ArticleId, UserId: UserId, 
                ArticleComment: ArticleComment, DateTime: DateTime);

            return command;
        }

        private CommentedArticle GetCommentedArticle()
        {
            var commentedArticle = new CommentedArticle(articleId: Guid.NewGuid(), userId: Guid.NewGuid(),
                articleComment: "...", dateTime: DateTimeOffset.Now);

            return commentedArticle;
        }

        public RemoveCommentedArticleHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new RemoveCommentedArticleHandler(_userRepository);
        }

        #endregion

        private Task Act(RemoveCommentedArticleCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeCommentedArticleCommand = GetRemoveCommentedArticleCommand();

            _userRepository.GetUserByIdAsync(removeCommentedArticleCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeCommentedArticleCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should remove CommentedArticle value object if the UserId from the RemoveCommentedArticleCommand is valid Id for existing User.
        //The removed CommentedArticle value object must have the same values as the values from the RemoveCommentedArticleCommand
        //and the repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_Removes_CommentedArticle_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var commentedArticle = GetCommentedArticle();

            var removeCommentedArticleCommand = GetRemoveCommentedArticleCommand(
                commentedArticle.ArticleID, commentedArticle.UserID, commentedArticle.ArticleComment, commentedArticle.DateTime);

            user.AddCommentedArticle(commentedArticle);

            _userRepository.GetUserByIdAsync(removeCommentedArticleCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeCommentedArticleCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
