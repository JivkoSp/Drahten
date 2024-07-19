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
    public class PublishDislikedArticleCommentTests : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly RabbitMqContainer _rabbitMqContainer;

        private DislikedArticleCommentDto GetDislikedArticleCommentDto()
        {
            var dislikedArticleCommentDto = new DislikedArticleCommentDto
            {
                ArticleCommentId = Guid.NewGuid(),
                ArticleId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                DateTime = DateTimeOffset.Now,
                Event = "DislikedArticleComment"
            };

            return dislikedArticleCommentDto;
        }

        public PublishDislikedArticleCommentTests(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _messageBusPublisher = factory.Services.GetRequiredService<IMessageBusPublisher>();
            _rabbitMqContainer = factory.RabbitMqContainer;
        }

        #endregion

        [Fact]
        public async Task Publish_DislikedArticleComment_Should_Add_DislikedArticleComment_With_Given_Id_To_Database()
        {
            //ARRANGE
            var dislikedArticleCommentDto = GetDislikedArticleCommentDto();

            //ACT
            await _messageBusPublisher.PublishDislikedArticleCommentAsync(dislikedArticleCommentDto);

            await Task.Delay(7000);

            //ASSERT

            var dislikedArticleCommentAddedEvent = IEventProcessor.Events.FirstOrDefault(
                x => x.GetType() == typeof(DislikedArticleCommentAdded)) as DislikedArticleCommentAdded;

            dislikedArticleCommentAddedEvent.ShouldNotBeNull();

            dislikedArticleCommentAddedEvent.ArticleCommentId.ShouldBe(dislikedArticleCommentDto.ArticleCommentId);
        }

        [Fact]
        public async Task Publish_Multiple_DislikedArticleComments_Should_Handle_Concurrency()
        {
            //ARRANGE
            var dislikedArticleCommentDtos = Enumerable.Range(0, 10).Select(_ => GetDislikedArticleCommentDto()).ToList();

            //ACT
            var tasks = dislikedArticleCommentDtos.Select(dto => _messageBusPublisher.PublishDislikedArticleCommentAsync(dto));

            await Task.WhenAll(tasks);

            await Task.Delay(5000);

            //ASSERT
            foreach (var dislikedArticleCommentDto in dislikedArticleCommentDtos)
            {
                var dislikedArticleCommentAddedEvent = IEventProcessor.Events.FirstOrDefault(
                    x => x is DislikedArticleCommentAdded added && added.ArticleCommentId == dislikedArticleCommentDto.ArticleCommentId) as DislikedArticleCommentAdded;

                dislikedArticleCommentAddedEvent.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Publish_LikedArticleComment_Should_Handle_RabbitMq_Down()
        {
            //ARRANGE
            var dislikedArticleCommentDto = GetDislikedArticleCommentDto();

            // Simulate RabbitMQ down
            await _rabbitMqContainer.StopAsync();

            // Capture console output
            using (var consoleOutput = new ConsoleOutput())
            {
                //ACT
                await _messageBusPublisher.PublishDislikedArticleCommentAsync(dislikedArticleCommentDto);

                //ASSERT
                var output = consoleOutput.GetOutput();

                output.ShouldContain("Unhandled exception during message sending");
            }

            // Restart RabbitMQ for other tests
            await _rabbitMqContainer.StartAsync();
        }
    }
}
