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
    public class PublishLikedArticleTests : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly RabbitMqContainer _rabbitMqContainer;

        private LikedArticleDto GetLikedArticleDto()
        {
            var likedArticleDto = new LikedArticleDto
            {
                ArticleId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                DateTime = DateTimeOffset.Now,
                Event = "LikedArticle"
            };

            return likedArticleDto;
        }

        public PublishLikedArticleTests(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _messageBusPublisher = factory.Services.GetRequiredService<IMessageBusPublisher>();
            _rabbitMqContainer = factory.RabbitMqContainer;
        }

        #endregion

        [Fact]
        public async Task Publish_LikedArticle_Should_Add_LikedArticle_With_Given_Id_To_Database()
        {
            //ARRANGE
            var likedArticleDto = GetLikedArticleDto();

            //ACT
            await _messageBusPublisher.PublishLikedArticleAsync(likedArticleDto);

            await Task.Delay(1000);

            //ASSERT

            var likedArticleAddedEvent = IEventProcessor.Events.FirstOrDefault(
                x => x.GetType() == typeof(LikedArticleAdded)) as LikedArticleAdded;


            likedArticleAddedEvent.ShouldNotBeNull();

            likedArticleAddedEvent.ArticleId.ShouldBe(likedArticleDto.ArticleId);
        }

        [Fact]
        public async Task Publish_Multiple_LikedArticles_Should_Handle_Concurrency()
        {
            //ARRANGE
            var likedArticleDtos = Enumerable.Range(0, 10).Select(_ => GetLikedArticleDto()).ToList();

            //ACT
            var tasks = likedArticleDtos.Select(dto => _messageBusPublisher.PublishLikedArticleAsync(dto));

            await Task.WhenAll(tasks);

            await Task.Delay(2000);

            //ASSERT
            foreach (var likedArticleDto in likedArticleDtos)
            {
                var likedArticleAddedEvent = IEventProcessor.Events.FirstOrDefault(
                    x => x is LikedArticleAdded added && added.ArticleId == likedArticleDto.ArticleId) as LikedArticleAdded;

                likedArticleAddedEvent.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Publish_LikedArticles_Should_Handle_RabbitMq_Down()
        {
            //ARRANGE
            var likedArticleDto = GetLikedArticleDto();

            // Simulate RabbitMQ down
            await _rabbitMqContainer.StopAsync();

            // Capture console output
            using (var consoleOutput = new ConsoleOutput())
            {
                //ACT
                await _messageBusPublisher.PublishLikedArticleAsync(likedArticleDto);

                //ASSERT
                var output = consoleOutput.GetOutput();

                output.ShouldContain("Unhandled exception during message sending");
            }

            // Restart RabbitMQ for other tests
            await _rabbitMqContainer.StartAsync();
        }
    }
}
