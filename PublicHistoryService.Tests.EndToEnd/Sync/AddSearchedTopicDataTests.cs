using Newtonsoft.Json;
using PublicHistoryService.Application.Commands;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Presentation.Dtos;
using PublicHistoryService.Tests.EndToEnd.Factories;
using Shouldly;
using System.Net;
using Xunit;

namespace PublicHistoryService.Tests.EndToEnd.Sync
{
    public class AddSearchedTopicDataTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddSearchedTopicDataCommand> PrepareAddSearchedTopicDataCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/publichistory-service/users/{userId}");

            var addSearchedTopicDataCommand = new AddSearchedTopicDataCommand(TopicId: Guid.NewGuid(), UserId: userId,
                SearchedData: "...", DateTime: DateTimeOffset.Now);

            return addSearchedTopicDataCommand;
        }

        public AddSearchedTopicDataTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if SearchedTopicData value object is created from the specified parameters in AddSearchedTopicDataCommand.
        [Fact]
        public async Task Add_Searched_Topic_Data_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddSearchedTopicDataCommandAsync();

            //ACT
            var response = await Post(command,
                $"/publichistory-service/users/{command.UserId}/searched-topics/{command.TopicId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/publichistory-service/users/{command.UserId}/searched-topics/{command.TopicId}");
        }

        //Should return http status code 200 when the previously created SearchedTopicData value object is successfully received from the database.
        [Fact]
        public async Task Add_Searched_Topic_Data_Endpoint_Should_Add_SearchedTopicData_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddSearchedTopicDataCommandAsync();

            //ACT
            await Post(command, $"/publichistory-service/users/{command.UserId}/searched-topics/{command.TopicId}");

            var searchedTopicsResponse = await Get($"/publichistory-service/users/{command.UserId}/searched-topics/");

            //ASSERT
            searchedTopicsResponse.ShouldNotBeNull();

            searchedTopicsResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await searchedTopicsResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<SearchedTopicDataDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var searchedTopicDataDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            searchedTopicDataDto.TopicId.ShouldBe(command.TopicId);

            searchedTopicDataDto.UserId.ShouldBe(command.UserId.ToString());

            searchedTopicDataDto.SearchedData.ShouldBe(command.SearchedData);

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - searchedTopicDataDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
