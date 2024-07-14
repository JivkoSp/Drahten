using NSubstitute;
using Shouldly;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using Xunit;


namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class CreateArticleHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IArticleRepository _articleRepository;
        private readonly IArticleFactory _articleConcreteFactory;
        private readonly IArticleFactory _articleMockFactory;
        private readonly IArticleReadService _articleService;
        private readonly ICommandHandler<CreateArticleCommand> _handler;

        private CreateArticleCommand GetCreateArticleCommand()
        {
            var command = new CreateArticleCommand(Guid.NewGuid(), "some prev title", "some title", "some content",
                "2022-08-10T14:38:00", "no author", "no link", Guid.NewGuid());

            return command;
        }
        
        public CreateArticleHandlerTests()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _articleConcreteFactory = new ArticleFactory();
            _articleMockFactory = Substitute.For<IArticleFactory>();
            _articleService = Substitute.For<IArticleReadService>();
            _handler = new CreateArticleHandler(_articleRepository, _articleMockFactory, _articleService);
        }

        #endregion

        private Task Act(CreateArticleCommand command)
            => _handler.HandleAsync(command);

        //Should throw ArticleAlreadyExistsException when the following condition is met:
        //There is already article domain entity with the same ArticleId as the ArticleId from the CreateArticleCommand.
        [Fact]
        public async Task DuplicateArticle_Throws_ArticleAlreadyExistsException()
        {
            //ARRANGE
            var command = GetCreateArticleCommand();

            _articleService.ExistsByIdAsync(command.ArticleId.ToString("N")).Returns(true);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(command));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ArticleAlreadyExistsException>();
        }

        //Should create new domain Article entity if there is NOT already article domain entity with the same
        //ArticleId as the ArticleId from the CreateArticleCommand (e.g If the ArticleId from the CreateArticleCommand is valid).
        //--------------------------------------------------------------------------
        //If the Article entity is created successfully the repository must also be called one time.
        //--------------------------------------------------------------------------
        //*** IMPORTANT *** - Check the _README.txt file in this directory for additional context about this method.
        [Fact]
        public async Task GivenValidArticleId_Calls_Repository_On_Success()
        {
            //ARRANGE
            var command = GetCreateArticleCommand();

            var article = _articleConcreteFactory.Create(command.ArticleId, command.PrevTitle, command.Title, command.Content,
                command.PublishingDate, command.Author, command.Link, command.TopicId);

            _articleService.ExistsByIdAsync(command.ArticleId.ToString("N")).Returns(false);

            //ACT
            _articleMockFactory.Create(command.ArticleId, command.PrevTitle, command.Title, command.Content, command.PublishingDate,
                   command.Author, command.Link, command.TopicId).Returns(article);

            var exception = await Record.ExceptionAsync(async () => await Act(command));

            //ASSERT
            exception.ShouldBeNull();

            _articleMockFactory.Received(1).Create(command.ArticleId, command.PrevTitle, command.Title, command.Content, command.PublishingDate,
                    command.Author, command.Link, command.TopicId);

            await _articleRepository.Received(1).AddArticleAsync(article);
        }
    }
}
