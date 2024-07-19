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
    public class PublishViewedArticleTests : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly RabbitMqContainer _rabbitMqContainer;

        private ViewedArticleDto GetViewedArticleDto()
        {
            var viewedArticleDto = new ViewedArticleDto
            {
                ArticleId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                DateTime = DateTimeOffset.Now,
                Event = "ViewedArticle"
            };

            return viewedArticleDto;
        }

        public PublishViewedArticleTests(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _messageBusPublisher = factory.Services.GetRequiredService<IMessageBusPublisher>();
            _rabbitMqContainer = factory.RabbitMqContainer;
        }

        #endregion

        [Fact]
        public async Task Publish_ViewedArticle_Should_Add_ViewedArticle_With_Given_Id_To_Database()
        {
            //ARRANGE
            var viewedArticleDto = GetViewedArticleDto();

            //ACT
            await _messageBusPublisher.PublishViewedArticleAsync(viewedArticleDto);

            await Task.Delay(1000);

            //ASSERT

            var viewedArticleAddedEvent = IEventProcessor.Events.FirstOrDefault(
                x => x.GetType() == typeof(ViewedArticleAdded)) as ViewedArticleAdded;

            viewedArticleAddedEvent.ShouldNotBeNull();

            viewedArticleAddedEvent.ArticleId.ShouldBe(viewedArticleDto.ArticleId);
        }

        [Fact]
        public async Task Publish_Multiple_ViewedArticles_Should_Handle_Concurrency()
        {
            //ARRANGE
            var viewedArticleDtos = Enumerable.Range(0, 10).Select(_ => GetViewedArticleDto()).ToList();

            //ACT
            var tasks = viewedArticleDtos.Select(dto => _messageBusPublisher.PublishViewedArticleAsync(dto));

            await Task.WhenAll(tasks);

            await Task.Delay(2000);

            //ASSERT
            foreach (var viewedArticleDto in viewedArticleDtos)
            {
                var viewedArticleAddedEvent = IEventProcessor.Events.FirstOrDefault(
                    x => x is ViewedArticleAdded added && added.ArticleId == viewedArticleDto.ArticleId) as ViewedArticleAdded;

                viewedArticleAddedEvent.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Publish_ViewedArticles_Should_Handle_RabbitMq_Down()
        {
            //ARRANGE
            var viewedArticleDto = GetViewedArticleDto();

            // Simulate RabbitMQ down
            await _rabbitMqContainer.StopAsync();

            // Capture console output
            using (var consoleOutput = new ConsoleOutput())
            {
                //ACT
                await _messageBusPublisher.PublishViewedArticleAsync(viewedArticleDto);

                //ASSERT
                var output = consoleOutput.GetOutput();

                output.ShouldContain("Unhandled exception during message sending");
            }

            // Restart RabbitMQ for other tests
            await _rabbitMqContainer.StartAsync();
        }
    }
}
