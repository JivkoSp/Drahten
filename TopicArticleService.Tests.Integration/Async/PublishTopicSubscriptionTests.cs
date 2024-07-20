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
    public class PublishTopicSubscriptionTests : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly RabbitMqContainer _rabbitMqContainer;

        private TopicSubscriptionDto GetTopicSubscriptionDto()
        {
            var topicSubscriptionDto = new TopicSubscriptionDto
            {
                TopicId = Guid.NewGuid(),
                UserId = Guid.NewGuid().ToString(),
                DateTime = DateTimeOffset.Now,
                Event = "TopicSubscription"
            };

            return topicSubscriptionDto;
        }

        public PublishTopicSubscriptionTests(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _messageBusPublisher = factory.Services.GetRequiredService<IMessageBusPublisher>();
            _rabbitMqContainer = factory.RabbitMqContainer;
        }

        #endregion

        [Fact]
        public async Task Publish_TopicSubscription_Should_Add_TopicSubscription_With_Given_Id_To_Database()
        {
            //ARRANGE
            var topicSubscriptionDto = GetTopicSubscriptionDto();

            //ACT
            await _messageBusPublisher.PublishTopicSubscriptionAsync(topicSubscriptionDto);

            await Task.Delay(1000);

            //ASSERT

            var topicSubscriptionAddedEvent = IEventProcessor.Events.FirstOrDefault(
                    x => x is TopicSubscriptionAdded added && added.TopicId == topicSubscriptionDto.TopicId) as TopicSubscriptionAdded;

            topicSubscriptionAddedEvent.ShouldNotBeNull();

            topicSubscriptionAddedEvent.TopicId.ShouldBe(topicSubscriptionDto.TopicId);
        }

        [Fact]
        public async Task Publish_Multiple_TopicSubscriptions_Should_Handle_Concurrency()
        {
            //ARRANGE
            var topicSubscriptionDtos = Enumerable.Range(0, 10).Select(_ => GetTopicSubscriptionDto()).ToList();

            //ACT
            var tasks = topicSubscriptionDtos.Select(dto => _messageBusPublisher.PublishTopicSubscriptionAsync(dto));

            await Task.WhenAll(tasks);

            await Task.Delay(2000);

            //ASSERT
            foreach (var topicSubscriptionDto in topicSubscriptionDtos)
            {
                var topicSubscriptionAddedEvent = IEventProcessor.Events.FirstOrDefault(
                    x => x is TopicSubscriptionAdded added && added.TopicId == topicSubscriptionDto.TopicId) as TopicSubscriptionAdded;

                topicSubscriptionAddedEvent.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Publish_TopicSubscription_Should_Handle_RabbitMq_Down()
        {
            //ARRANGE
            var topicSubscriptionDto = GetTopicSubscriptionDto();

            // Simulate RabbitMQ down
            await _rabbitMqContainer.StopAsync();

            // Capture console output
            using (var consoleOutput = new ConsoleOutput())
            {
                //ACT
                await _messageBusPublisher.PublishTopicSubscriptionAsync(topicSubscriptionDto);

                //ASSERT
                var output = consoleOutput.GetOutput();

                output.ShouldContain("Unhandled exception during message sending");
            }

            // Restart RabbitMQ for other tests
            await _rabbitMqContainer.StartAsync();
        }
    }
}
