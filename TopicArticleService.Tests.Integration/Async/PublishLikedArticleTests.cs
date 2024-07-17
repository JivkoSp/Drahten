using Microsoft.Extensions.DependencyInjection;
using Shouldly;
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
    }
}
