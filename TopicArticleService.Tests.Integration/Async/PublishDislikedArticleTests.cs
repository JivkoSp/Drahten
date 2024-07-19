using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Tests.Integration.EventProcessing;
using TopicArticleService.Tests.Integration.Events;
using TopicArticleService.Tests.Integration.Factories;
using Xunit;

namespace TopicArticleService.Tests.Integration.Async
{
    public class PublishDislikedArticleTests : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly IMessageBusPublisher _messageBusPublisher;

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
    }
}
