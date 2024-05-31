using Newtonsoft.Json;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Presentation.Dtos;
using PrivateHistoryService.Tests.EndToEnd.Factories;
using Shouldly;
using System.Net;
using Xunit;

namespace PrivateHistoryService.Tests.EndToEnd.Sync
{
    public class AddTopicSubscriptionTests : BaseSyncIntegrationTest
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

        public AddTopicSubscriptionTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if TopicSubscription value object is created from the specified parameters in AddTopicSubscriptionCommand.
        [Fact]
        public async Task Add_Topic_Subscription_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddTopicSubscriptionCommandAsync();

            //ACT
            var response = await Post(command,
                $"/privatehistory-service/users/{command.UserId}/topic-subscriptions/{command.TopicId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/privatehistory-service/users/{command.UserId}/topic-subscriptions/{command.TopicId}");
        }

        //Should return http status code 200 when the previously created TopicSubscription value object is successfully received from the database.
        [Fact]
        public async Task Add_Topic_Subscription_Endpoint_Should_Add_TopicSubscription_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddTopicSubscriptionCommandAsync();

            //ACT
            await Post(command, $"/privatehistory-service/users/{command.UserId}/topic-subscriptions/{command.TopicId}");

            var topicSubscriptionsResponse = await Get($"/privatehistory-service/users/{command.UserId}/topic-subscriptions/");

            //ASSERT
            topicSubscriptionsResponse.ShouldNotBeNull();

            topicSubscriptionsResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await topicSubscriptionsResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<TopicSubscriptionDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var topicSubscriptionDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            topicSubscriptionDto.TopicId.ShouldBe(command.TopicId);

            topicSubscriptionDto.UserId.ShouldBe(command.UserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - topicSubscriptionDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
