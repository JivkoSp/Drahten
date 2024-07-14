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
using TopicArticleService.Application.AsyncDataServices;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class AddArticleDislikeHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IArticleRepository _articleRepository;
        private readonly IArticleFactory _articleConcreteFactory;
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly ICommandHandler<AddArticleDislikeCommand> _handler;

        private AddArticleDislikeCommand GetAddArticleDislikeCommand(ArticleID articleId)
        {
            var command = new AddArticleDislikeCommand(articleId, DateTimeOffset.Now, Guid.NewGuid());

            return command;
        }

        public AddArticleDislikeHandlerTests()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _articleConcreteFactory = new ArticleFactory();
            _messageBusPublisher = Substitute.For<IMessageBusPublisher>();
            _handler = new AddArticleDislikeHandler(_articleRepository, _messageBusPublisher);
        }

        #endregion

        private Task Act(AddArticleDislikeCommand command)
           => _handler.HandleAsync(command);

        //Should throw ArticleNotFoundException when the following condition is met:
        //There is no article returned from the repository.
        //The reason can be that there is no article that has ArticleId like the ArticleId from the AddArticleDislikeCommand
        //or any other reason like network problems for example.  
        [Fact]
        public async Task Throws_ArticleNotFoundException_When_Article_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var article = _articleConcreteFactory.Create(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            var addArticleDislikeCommand = GetAddArticleDislikeCommand(article.Id);

            _articleRepository.GetArticleByIdAsync(addArticleDislikeCommand.ArticleId).Returns(default(Article));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addArticleDislikeCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleNotFoundException>();
        }

        //Should create new ArticleDislike value object if the ArticleId from the AddArticleDislikeCommand is valid Id for existing article.
        //Additionally the created ArticleDislike value object must be added to the Article entity and the repository must be called to
        //update the Article entity.
        [Fact]
        public async Task Given_Valid_ArticleId_Creates_And_Adds_ArticleDislike_Instance_To_Article_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var article = _articleConcreteFactory.Create(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            var addArticleDislikeCommand = GetAddArticleDislikeCommand(article.Id);

            _articleRepository.GetArticleByIdAsync(addArticleDislikeCommand.ArticleId).Returns(article);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addArticleDislikeCommand));

            //ASSERT
            exception.ShouldBeNull();

            _messageBusPublisher.Received(1);

            await _articleRepository.Received(1).UpdateArticleAsync(article);
        }
    }
}
