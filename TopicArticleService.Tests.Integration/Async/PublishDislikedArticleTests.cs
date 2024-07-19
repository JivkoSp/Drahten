using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Exceptions;
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
    public class PublishDislikedArticleTests : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly RabbitMqContainer _rabbitMqContainer;

        private DislikedArticleDto GetDislikedArticleDto()
        {
            var dislikedArticleDto = new DislikedArticleDto
            {
                ArticleId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                DateTime = DateTimeOffset.Now,
                Event = "DislikedArticle"
            };

            return dislikedArticleDto;
        }

        public PublishDislikedArticleTests(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _messageBusPublisher = factory.Services.GetRequiredService<IMessageBusPublisher>();
            _rabbitMqContainer = factory.RabbitMqContainer;
        }

        #endregion

        [Fact]
        public async Task Publish_DislikedArticle_Should_Add_DislikedArticle_With_Given_Id_To_Database()
        {
            //ARRANGE
            var dislikedArticleDto = GetDislikedArticleDto();

            //ACT
            await _messageBusPublisher.PublishDislikedArticleAsync(dislikedArticleDto);

            await Task.Delay(1000);

            //ASSERT

            var dislikedArticleAddedEvent = IEventProcessor.Events.FirstOrDefault(
                x => x.GetType() == typeof(DislikedArticleAdded)) as DislikedArticleAdded;

            dislikedArticleAddedEvent.ShouldNotBeNull();

            dislikedArticleAddedEvent.ArticleId.ShouldBe(dislikedArticleDto.ArticleId);
        }

        [Fact]
        public async Task Publish_Multiple_DislikedArticles_Should_Handle_Concurrency()
        {
            //ARRANGE
            var dislikedArticleDtos = Enumerable.Range(0, 10).Select(_ => GetDislikedArticleDto()).ToList();

            //ACT
            var tasks = dislikedArticleDtos.Select(dto => _messageBusPublisher.PublishDislikedArticleAsync(dto));

            await Task.WhenAll(tasks);

            await Task.Delay(1000);

            //ASSERT
            foreach (var dislikedArticleDto in dislikedArticleDtos)
            {
                var dislikedArticleAddedEvent = IEventProcessor.Events.FirstOrDefault(
                    x => x is DislikedArticleAdded added && added.ArticleId == dislikedArticleDto.ArticleId) as DislikedArticleAdded;

                dislikedArticleAddedEvent.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Publish_DislikedArticle_Should_Handle_RabbitMq_Down()
        {
            //ARRANGE
            var dislikedArticleDto = GetDislikedArticleDto();

            // Simulate RabbitMQ down
            await _rabbitMqContainer.StopAsync();

            // Capture console output
            using (var consoleOutput = new ConsoleOutput())
            {
                //ACT
                await _messageBusPublisher.PublishDislikedArticleAsync(dislikedArticleDto);

                //ASSERT
                var output = consoleOutput.GetOutput();

                output.ShouldContain("Unhandled exception during message sending");
            }

            // Restart RabbitMQ for other tests
            await _rabbitMqContainer.StartAsync();
        }
    }
}
