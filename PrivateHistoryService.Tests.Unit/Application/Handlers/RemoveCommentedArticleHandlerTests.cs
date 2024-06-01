using NSubstitute;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Services.ReadServices;
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
        private readonly ICommentedArticleReadService _commentedArticleReadService;
        private readonly ICommandHandler<RemoveCommentedArticleCommand> _handler;

        private RemoveCommentedArticleCommand GetRemoveCommentedArticleCommand(Guid? userId = null, Guid? commentedArticleId = null)
        {
            var UserId = userId ?? Guid.NewGuid();
            var CommentedArticleId = commentedArticleId ?? Guid.NewGuid();

            var command = new RemoveCommentedArticleCommand(UserId: UserId, CommentedArticleId: CommentedArticleId);

            return command;
        }

        private CommentedArticle GetCommentedArticle()
        {
            var commentedArticle = new CommentedArticle(articleId: Guid.NewGuid(), userId: Guid.NewGuid(),
                articleComment: "...", dateTime: DateTimeOffset.Now);

            return commentedArticle;
        }

        private CommentedArticleDto GetCommentedArticleDto(CommentedArticle commentedArticle)
        {
            var commentedArticleDto = new CommentedArticleDto
            { 
                CommentedArticleId= Guid.NewGuid(),
                ArticleId = commentedArticle.ArticleID.Value.ToString(),
                UserId = commentedArticle.UserID.Value.ToString(),
                ArticleComment= commentedArticle.ArticleComment,
                DateTime = commentedArticle.DateTime
            };

            return commentedArticleDto;
        }

        public RemoveCommentedArticleHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _commentedArticleReadService = Substitute.For<ICommentedArticleReadService>();
            _handler = new RemoveCommentedArticleHandler(_userRepository, _commentedArticleReadService);
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

        //Should throw CommentedArticleNotFoundException when the following condition is met:
        //There is no CommentedArticle returned from the repository that corresponds to the CommentedArticleId from the command.
        [Fact]
        public async Task Throws_CommentedArticleNotFoundException_When_CommentedArticle_With_Given_CommentedArticleId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var removeCommentedArticleCommand = GetRemoveCommentedArticleCommand();

            _userRepository.GetUserByIdAsync(removeCommentedArticleCommand.UserId).Returns(user);

            _commentedArticleReadService.GetCommentedArticleByIdAsync(removeCommentedArticleCommand.CommentedArticleId)
                .Returns(default(CommentedArticleDto));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeCommentedArticleCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<CommentedArticleNotFoundException>();
        }

        //Should remove CommentedArticle value object if the UserId and CommentedArticleId from the RemoveCommentedArticleCommand
        //are valid Id's for existing User domain entity and CommentedArticle value object.
        //The repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_And_CommentedArticleId_Removes_CommentedArticle_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var commentedArticle = GetCommentedArticle();

            var commentedArticleDto = GetCommentedArticleDto(commentedArticle);

            var removeCommentedArticleCommand = GetRemoveCommentedArticleCommand();

            user.AddCommentedArticle(commentedArticle);

            _userRepository.GetUserByIdAsync(removeCommentedArticleCommand.UserId).Returns(user);

            _commentedArticleReadService.GetCommentedArticleByIdAsync(removeCommentedArticleCommand.CommentedArticleId)
                .Returns(commentedArticleDto);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeCommentedArticleCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
