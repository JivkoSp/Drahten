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
    public class PublishLikedArticleComment : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly RabbitMqContainer _rabbitMqContainer;

        private LikedArticleCommentDto GetLikedArticleCommentDto()
        {
            var likedArticleCommentDto = new LikedArticleCommentDto
            {
                ArticleCommentId = Guid.NewGuid(),
                ArticleId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                DateTime = DateTimeOffset.Now,
                Event = "LikedArticleComment"
            };

            return likedArticleCommentDto;
        }

        public PublishLikedArticleComment(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _messageBusPublisher = factory.Services.GetRequiredService<IMessageBusPublisher>();
            _rabbitMqContainer = factory.RabbitMqContainer;
        }

        #endregion

        [Fact]
        public async Task Publish_LikedArticleComment_Should_Add_LikedArticleComment_With_Given_Id_To_Database()
        {
            //ARRANGE
            var likedArticleCommentDto = GetLikedArticleCommentDto();

            //ACT
            await _messageBusPublisher.PublishLikedArticleCommentAsync(likedArticleCommentDto);

            await Task.Delay(2000);

            //ASSERT

            var likedArticleCommentAddedEvent = IEventProcessor.Events.FirstOrDefault(
                x => x.GetType() == typeof(LikedArticleCommentAdded)) as LikedArticleCommentAdded;

            likedArticleCommentAddedEvent.ShouldNotBeNull();

            likedArticleCommentAddedEvent.ArticleCommentId.ShouldBe(likedArticleCommentDto.ArticleCommentId);
        }

        [Fact]
        public async Task Publish_Multiple_LikedArticleComments_Should_Handle_Concurrency()
        {
            //ARRANGE
            var likedArticleCommentDtos = Enumerable.Range(0, 10).Select(_ => GetLikedArticleCommentDto()).ToList();

            //ACT
            var tasks = likedArticleCommentDtos.Select(dto => _messageBusPublisher.PublishLikedArticleCommentAsync(dto));

            await Task.WhenAll(tasks);

            await Task.Delay(2000);

            //ASSERT
            foreach (var likedArticleCommentDto in likedArticleCommentDtos)
            {
                var likedArticleCommentAddedEvent = IEventProcessor.Events.FirstOrDefault(
                    x => x is LikedArticleCommentAdded added && added.ArticleCommentId == likedArticleCommentDto.ArticleCommentId) as LikedArticleCommentAdded;

                likedArticleCommentAddedEvent.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Publish_LikedArticleComment_Should_Handle_RabbitMq_Down()
        {
            //ARRANGE
            var likedArticleCommentDto = GetLikedArticleCommentDto();

            // Simulate RabbitMQ down
            await _rabbitMqContainer.StopAsync();

            // Capture console output
            using (var consoleOutput = new ConsoleOutput())
            {
                //ACT
                await _messageBusPublisher.PublishLikedArticleCommentAsync(likedArticleCommentDto);

                //ASSERT
                var output = consoleOutput.GetOutput();

                output.ShouldContain("Unhandled exception during message sending");
            }

            // Restart RabbitMQ for other tests
            await _rabbitMqContainer.StartAsync();
        }
    }
}
