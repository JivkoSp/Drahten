using Newtonsoft.Json;
using Shouldly;
using System.Net;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Presentation.Dtos;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RegisterUserTopicTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        //*** IMPORTANT *** - The RegisterUserTopicCommand must have valid TopicId (TopicId for Topic that already exist in the database).

        private async Task<RegisterUserTopicCommand> PrepareRegisterUserTopicCommandAsync()
        {
            var registerUserCommand = new RegisterUserCommand(Guid.NewGuid());

            await Post(registerUserCommand, "/topic-article-service/users");

            var registerUserTopicCommand = new RegisterUserTopicCommand(registerUserCommand.UserId, 
                Guid.Parse("e0e68a89-8cb2-4602-a10b-2be1a78a9be5"), DateTimeOffset.Now);
   
            return registerUserTopicCommand;
        }

        public RegisterUserTopicTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if UserTopic value object is created from the specified parameters in RegisterUserTopicCommand.
        [Fact]
        public async Task Register_UserTopic_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var registerUserTopicCommand = await PrepareRegisterUserTopicCommandAsync();

            //ACT
            var response = await Post(registerUserTopicCommand, $"/topic-article-service/users/{registerUserTopicCommand.UserId}/topics/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe($"/topic-article-service/users/{registerUserTopicCommand.UserId}/topics/");
        }

        //Should return http status code 200 when the previously created UserTopic value object is successfully received from the database.
        [Fact]
        public async Task Register_UserTopic_Endpoint_Should_Add_UserTopic_With_Given_UserId_And_TopicId_To_The_Database()
        {
            //ARRANGE
            var registerUserTopicCommand = await PrepareRegisterUserTopicCommandAsync();

            await Post(registerUserTopicCommand, $"/topic-article-service/users/{registerUserTopicCommand.UserId}/topics/");

            //ACT
            var response = await Get($"/topic-article-service/topics/{registerUserTopicCommand.UserId}/user-topics/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var userTopicDto = JsonConvert.DeserializeObject<List<UserTopicDto>>(Convert.ToString(responseDto.Result)).FirstOrDefault();

            userTopicDto.ShouldNotBeNull();

            userTopicDto.UserId.ShouldBe(registerUserTopicCommand.UserId.ToString());

            userTopicDto.TopicId.ShouldBe(registerUserTopicCommand.TopicId);

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((registerUserTopicCommand.DateTime - userTopicDto.SubscriptionTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
