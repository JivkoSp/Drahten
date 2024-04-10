using NSubstitute;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Commands;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Application.Exceptions;
using Xunit;
using TopicArticleService.Domain.Entities;
using Shouldly;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class AddArticleCommentHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IArticleRepository _articleRepository;
        private readonly IArticleFactory _articleConcreteFactory;
        private readonly IArticleCommentFactory _articleCommentConcreteFactory;
        private readonly IArticleCommentFactory _articleCommentMockFactory;
        private readonly ICommandHandler<AddArticleCommentCommand> _handler;

        private AddArticleCommentCommand GetAddArticleCommentCommand(ArticleID articleId)
        {
            var command = new AddArticleCommentCommand(articleId, Guid.NewGuid(), "some comment", DateTimeOffset.Now, 
                Guid.NewGuid(), null);

            return command;
        }

        public AddArticleCommentHandlerTests()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _articleConcreteFactory = new ArticleFactory();
            _articleCommentConcreteFactory = new ArticleCommentFactory();
            _articleCommentMockFactory = Substitute.For<IArticleCommentFactory>();
            _handler = new AddArticleCommentHandler(_articleRepository, _articleCommentMockFactory);
        }

        #endregion

        private Task Act(AddArticleCommentCommand command)
           => _handler.HandleAsync(command);

        //Should throw ArticleNotFoundException when the following condition is met:
        //There is no article returned from the repository.
        //The reason can be that there is no article that has ArticleId like the ArticleId from the AddArticleCommentCommand
        //or any other reason like network problems for example.  
        [Fact]
        public async Task Throws_ArticleNotFoundException_When_Article_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var article = _articleConcreteFactory.Create(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            var addArticleCommentCommand = GetAddArticleCommentCommand(article.Id);

            _articleRepository.GetArticleByIdAsync(addArticleCommentCommand.ArticleId).Returns(default(Article));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addArticleCommentCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleNotFoundException>();
        }

        //Should create new ArticleComment domain entity if the ArticleId from the AddArticleCommentCommand is valid Id for existing article.
        //Additionally the created ArticleComment domain entity must be added to the Article domain entity and the repository
        //must be called to update the Article entity.
        [Fact]
        public async Task Given_Valid_ArticleId_Creates_And_Adds_ArticleComment_Instance_To_Article_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var article = _articleConcreteFactory.Create(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            var addArticleCommentCommand = GetAddArticleCommentCommand(article.Id);

            var articleComment = _articleCommentConcreteFactory.Create(addArticleCommentCommand.ArticleCommentId,
                addArticleCommentCommand.CommentValue, addArticleCommentCommand.DateTime, addArticleCommentCommand.UserId,
                addArticleCommentCommand.ParentArticleCommentId);

            _articleRepository.GetArticleByIdAsync(addArticleCommentCommand.ArticleId).Returns(article);

            //ACT
            _articleCommentMockFactory.Create(addArticleCommentCommand.ArticleCommentId,
                addArticleCommentCommand.CommentValue, addArticleCommentCommand.DateTime, addArticleCommentCommand.UserId,
                addArticleCommentCommand.ParentArticleCommentId).Returns(articleComment);

            var exception = await Record.ExceptionAsync(async () => await Act(addArticleCommentCommand));

            //ASSERT
            exception.ShouldBeNull();

            _articleCommentMockFactory.Received(1).Create(addArticleCommentCommand.ArticleCommentId,
                addArticleCommentCommand.CommentValue, addArticleCommentCommand.DateTime, addArticleCommentCommand.UserId,
                addArticleCommentCommand.ParentArticleCommentId);

            await _articleRepository.Received(1).UpdateArticleAsync(article);
        }
    }
}
