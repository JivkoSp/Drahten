using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Testcontainers.RabbitMq;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Tests.Integration.EventProcessing;
using TopicArticleService.Tests.Integration.Events;
using TopicArticleService.Tests.Integration.Factories;
using TopicArticleService.Tests.Integration.Services;
using Xunit;

namespace TopicArticleService.Tests.Integration.Async
{
    public class PublishCommentedArticleTests : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly RabbitMqContainer _rabbitMqContainer;

        private CommentedArticleDto GetCommentedArticleDto()
        {
            var commentedArticleDto = new CommentedArticleDto
            {
                ArticleId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                ArticleComment = "",
                DateTime = DateTimeOffset.Now,
                Event = "CommentedArticle"
            };

            return commentedArticleDto;
        }

        public PublishCommentedArticleTests(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _messageBusPublisher = factory.Services.GetRequiredService<IMessageBusPublisher>();
            _rabbitMqContainer = factory.RabbitMqContainer;
        }

        #endregion

        [Fact]
        public async Task Publish_CommentedArticle_Should_Add_CommentedArticle_With_Given_Id_To_Database()
        {
            //ARRANGE
            var commentedArticleDto = GetCommentedArticleDto();

            //ACT
            await _messageBusPublisher.PublishCommentedArticleAsync(commentedArticleDto);

            await Task.Delay(1000);

            //ASSERT

            var commentedArticleAddedEvent = IEventProcessor.Events.FirstOrDefault(
                x => x.GetType() == typeof(CommentedArticleAdded)) as CommentedArticleAdded;

            commentedArticleAddedEvent.ShouldNotBeNull();

            commentedArticleAddedEvent.ArticleId.ShouldBe(commentedArticleDto.ArticleId);
        }

        [Fact]
        public async Task Publish_Multiple_CommentedArticles_Should_Handle_Concurrency()
        {
            //ARRANGE
            var commentedArticleDtos = Enumerable.Range(0, 10).Select(_ => GetCommentedArticleDto()).ToList();

            //ACT
            var tasks = commentedArticleDtos.Select(dto => _messageBusPublisher.PublishCommentedArticleAsync(dto));

            await Task.WhenAll(tasks);

            await Task.Delay(2000);

            //ASSERT
            foreach (var commentedArticleDto in commentedArticleDtos)
            {
                var commentedArticleAddedEvent = IEventProcessor.Events.FirstOrDefault(
                    x => x is CommentedArticleAdded added && added.ArticleId == commentedArticleDto.ArticleId) as CommentedArticleAdded;

                commentedArticleAddedEvent.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Publish_CommentedArticle_Should_Handle_RabbitMq_Down()
        {
            //ARRANGE
            var commentedArticleDto = GetCommentedArticleDto();

            // Simulate RabbitMQ down
            await _rabbitMqContainer.StopAsync();

            // Capture console output
            using (var consoleOutput = new ConsoleOutput())
            {
                //ACT
                await _messageBusPublisher.PublishCommentedArticleAsync(commentedArticleDto);

                //ASSERT
                var output = consoleOutput.GetOutput();

                output.ShouldContain("Unhandled exception during message sending");
            }

            // Restart RabbitMQ for other tests
            await _rabbitMqContainer.StartAsync();
        }
    }
}
