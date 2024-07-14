using NSubstitute;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Commands;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using Xunit;
using TopicArticleService.Domain.Entities;
using Shouldly;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.AsyncDataServices;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class RegisterUserArticleHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IArticleRepository _articleRepository;
        private readonly IArticleFactory _articleConcreteFactory;
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly ICommandHandler<RegisterUserArticleCommand> _handler;

        private CreateArticleCommand GetCreateArticleCommand()
        {
            var command = new CreateArticleCommand(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            return command;
        }

        private RegisterUserArticleCommand GetRegisterUserArticleCommand()
        {
            var command = new RegisterUserArticleCommand(ArticleId: Guid.NewGuid(), UserId: Guid.NewGuid());

            return command;
        }

        public RegisterUserArticleHandlerTests()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _articleConcreteFactory = new ArticleFactory();
            _messageBusPublisher = Substitute.For<IMessageBusPublisher>();
            _handler = new RegisterUserArticleHandler(_articleRepository, _messageBusPublisher);
        }

        #endregion

        private Task Act(RegisterUserArticleCommand command)
            => _handler.HandleAsync(command);

        //Should throw ArticleNotFoundException when the following condition is met:
        //There is no article returned from the repository.
        //The reason can be that there is no article that has ArticleId like the ArticleId from the RegisterUserArticleCommand or any other
        //reason like network problems for example.  
        [Fact]
        public async Task Throws_ArticleNotFoundException_When_Article_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var command = GetRegisterUserArticleCommand();

            _articleRepository.GetArticleByIdAsync(command.ArticleId).Returns(default(Article));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(command));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleNotFoundException>();
        }

        //Should create new UserArticle value object if the ArticleId from the RegisterUserArticleCommand is valid Id for existing article.
        //Additionally the created UserArticle value object must be added to the Article entity and the repository must be called to
        //update the Article entity.
        [Fact]
        public async Task Given_Valid_ArticleId_Creates_And_Adds_UserArticle_Instance_To_Article_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var createArticleCommand = GetCreateArticleCommand();

            var registerUserArticleCommand = GetRegisterUserArticleCommand();

            var article = _articleConcreteFactory.Create(createArticleCommand.ArticleId, createArticleCommand.PrevTitle, 
                createArticleCommand.Title, createArticleCommand.Content, createArticleCommand.PublishingDate, createArticleCommand.Author,
                createArticleCommand.Link, createArticleCommand.TopicId);

            _articleRepository.GetArticleByIdAsync(registerUserArticleCommand.ArticleId).Returns(article);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(registerUserArticleCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _articleRepository.Received(1).UpdateArticleAsync(article);
        }
    }


    
}
