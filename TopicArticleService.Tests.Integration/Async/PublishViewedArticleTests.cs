using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
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
        private readonly RabbitMqMessageBusSubscriber _rabbitMqMessageBusSubscriber;

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
            _rabbitMqMessageBusSubscriber = factory.Services.GetRequiredService<RabbitMqMessageBusSubscriber>();
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

            var viewedArticleAddedEvent = _rabbitMqMessageBusSubscriber.Events.FirstOrDefault(
                x => x.GetType() == typeof(ViewedArticleAdded)) as ViewedArticleAdded;


            viewedArticleAddedEvent.ShouldNotBeNull();

            viewedArticleAddedEvent.ArticleId.ShouldBe(viewedArticleDto.ArticleId);
        }
    }
}
