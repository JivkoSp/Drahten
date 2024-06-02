using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Tests.EndToEnd.Factories;
using Shouldly;
using System.Net;
using Xunit;

namespace PrivateHistoryService.Tests.EndToEnd.Sync
{
    public class RemoveTopicSubscriptionTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddTopicSubscriptionCommand> PrepareAddTopicSubscriptionCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/privatehistory-service/users/{userId}");

            var addTopicSubscriptionCommand = new AddTopicSubscriptionCommand(TopicId: Guid.NewGuid(), UserId: userId,
                DateTime: DateTimeOffset.Now);

            return addTopicSubscriptionCommand;
        }

        private async Task<RemoveTopicSubscriptionCommand> PrepareRemoveTopicSubscriptionCommandAsync()
        {
            var command = await PrepareAddTopicSubscriptionCommandAsync();

            await Post(command, $"/privatehistory-service/users/{command.UserId}/topic-subscriptions/{command.TopicId}");

            var removeTopicSubscriptionCommand = new RemoveTopicSubscriptionCommand(TopicId: command.TopicId, UserId: command.UserId);

            return removeTopicSubscriptionCommand;
        }

        public RemoveTopicSubscriptionTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if TopicSubscription that has TopicId and UserId values like the ones that are
        //specified in RemoveTopicSubscriptionCommand is deleted from the database.
        [Fact]
        public async Task Remove_Topic_Subscription_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var command = await PrepareRemoveTopicSubscriptionCommandAsync();

            //ACT
            var response = await Delete($"/privatehistory-service/users/{command.UserId}/topic-subscriptions/{command.TopicId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted TopicSubscription value object is not found in the database.
        [Fact]
        public async Task Remove_Topic_Subscription_Endpoint_Should_Remove_TopicSubscription_From_The_Database()
        {
            //ARRANGE
            var command = await PrepareRemoveTopicSubscriptionCommandAsync();

            await Delete($"/privatehistory-service/users/{command.UserId}/topic-subscriptions/{command.TopicId}");

            //ACT
            var response = await Get($"/privatehistory-service/users/{command.UserId}/topic-subscriptions/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
