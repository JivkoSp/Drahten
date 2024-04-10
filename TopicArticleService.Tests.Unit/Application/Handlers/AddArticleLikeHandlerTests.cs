using NSubstitute;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Commands;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;
using TopicArticleService.Domain.Entities;
using Shouldly;
using TopicArticleService.Application.Exceptions;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class AddArticleLikeHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IArticleRepository _articleRepository;
        private readonly IArticleFactory _articleConcreteFactory;
        private readonly IArticleLikeFactory _articleLikeMockFactory;
        private readonly ICommandHandler<AddArticleLikeCommand> _handler;

        private AddArticleLikeCommand GetAddArticleLikeCommand(ArticleID articleId)
        {
            var command = new AddArticleLikeCommand(articleId, DateTimeOffset.Now, Guid.NewGuid());

            return command;
        }

        public AddArticleLikeHandlerTests()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _articleConcreteFactory = new ArticleFactory();
            _articleLikeMockFactory = Substitute.For<IArticleLikeFactory>();
            _handler = new AddArticleLikeHandler(_articleRepository, _articleLikeMockFactory);
        }

        #endregion

        private Task Act(AddArticleLikeCommand command)
            => _handler.HandleAsync(command);

        //Should throw ArticleNotFoundException when the following condition is met:
        //There is no article returned from the repository.
        //The reason can be that there is no article that has ArticleId like the ArticleId from the AddArticleLikeCommand or any other
        //reason like network problems for example.  
        [Fact]
        public async Task Throws_ArticleNotFoundException_When_Article_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var article = _articleConcreteFactory.Create(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            var addArticleLikeCommand = GetAddArticleLikeCommand(article.Id);

            _articleRepository.GetArticleByIdAsync(addArticleLikeCommand.ArticleId).Returns(default(Article));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addArticleLikeCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleNotFoundException>();
        }

        //Should create new ArticleLike value object if the ArticleId from the AddArticleLikeCommand is valid Id for existing article.
        //Additionally the created ArticleLike value object must be added to the Article entity and the repository must be called to
        //update the Article entity.
        [Fact]
        public async Task Given_Valid_ArticleId_Creates_And_Adds_ArticleLike_Instance_To_Article_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var article = _articleConcreteFactory.Create(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            var addArticleLikeCommand = GetAddArticleLikeCommand(article.Id);

            _articleRepository.GetArticleByIdAsync(addArticleLikeCommand.ArticleId).Returns(article);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addArticleLikeCommand));

            //ASSERT
            exception.ShouldBeNull();

            _articleLikeMockFactory.Received(1).Create(addArticleLikeCommand.ArticleId, 
                addArticleLikeCommand.UserId, addArticleLikeCommand.DateTime);

            await _articleRepository.Received(1).UpdateArticleAsync(article);
        }
    }
}
