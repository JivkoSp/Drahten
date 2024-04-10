using NSubstitute;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Commands;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using Xunit;
using TopicArticleService.Domain.Entities;
using Shouldly;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class RemoveArticleCommentHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IArticleRepository _articleRepository;
        private readonly IArticleFactory _articleConcreteFactory;
        private readonly IArticleCommentFactory _articleCommentConcreteFactory;
        private readonly ICommandHandler<RemoveArticleCommentCommand> _handler;

        private ArticleComment GetArticleCommentWithoutParentComment()
        {
            var articleComment = _articleCommentConcreteFactory.Create(Guid.NewGuid(), "some comment", DateTimeOffset.Now,
                    Guid.NewGuid(), null);

            articleComment.ClearEvents();

            return articleComment;
        }

        private RemoveArticleCommentCommand GetRemoveArticleCommentCommand(ArticleID articleId = null, 
            ArticleCommentID articleCommentId = null)
        {
            articleId = articleId ?? Guid.NewGuid();

            articleCommentId = articleCommentId ?? Guid.NewGuid();  

            var command = new RemoveArticleCommentCommand(articleId, articleCommentId);

            return command;
        }

        public RemoveArticleCommentHandlerTests()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _articleConcreteFactory = new ArticleFactory();
            _articleCommentConcreteFactory = new ArticleCommentFactory();
            _handler = new RemoveArticleCommentHandler(_articleRepository);
        }

        #endregion

        private Task Act(RemoveArticleCommentCommand command)
           => _handler.HandleAsync(command);

        //Should throw ArticleNotFoundException when the following condition is met:
        //There is no article returned from the repository.
        //The reason can be that there is no article that has ArticleId like the ArticleId from the RemoveArticleCommentCommand
        //or any other reason like network problems for example.  
        [Fact]
        public async Task Throws_ArticleNotFoundException_When_Article_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeArticleCommentCommand = GetRemoveArticleCommentCommand();

            _articleRepository.GetArticleByIdAsync(removeArticleCommentCommand.ArticleId).Returns(default(Article));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeArticleCommentCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleNotFoundException>();
        }

        //Should remove the ArticleComment, specified by the RemoveArticleCommentCommand object if the ArticleId from
        //RemoveArticleCommentCommand is valid Id for existing article.
        //Additionally the repository must be called to update the Article entity.
        [Fact]
        public async Task Given_Valid_ArticleId_Removes_ArticleComment_Instance_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var article = _articleConcreteFactory.Create(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            var articleComment = GetArticleCommentWithoutParentComment();

            article.AddComment(articleComment);

            var removeArticleCommentCommand = GetRemoveArticleCommentCommand(article.Id, articleComment.Id);

            _articleRepository.GetArticleByIdAsync(removeArticleCommentCommand.ArticleId).Returns(article);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeArticleCommentCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _articleRepository.Received(1).UpdateArticleAsync(article);
        }
    }
}
