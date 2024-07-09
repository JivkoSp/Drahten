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
    public class RemoveSearchedTopicDataTests : BaseSyncIntegrationTest
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

        private async Task<SearchedTopicDataDto> GetSearchedTopicDataDtoAsync(Guid userId)
        {
            var searchedTopicsResponse = await Get($"/publichistory-service/users/{userId}/searched-topics/");

            var responseSerializedContent = await searchedTopicsResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            var responseResult = JsonConvert.DeserializeObject<List<SearchedTopicDataDto>>(Convert.ToString(responseDto.Result));

            return responseResult.FirstOrDefault();
        }

        private async Task<RemoveSearchedTopicDataCommand> PrepareRemoveSearchedTopicDataCommandAsync()
        {
            var command = await PrepareAddSearchedTopicDataCommandAsync();

            await Post(command, $"/publichistory-service/users/{command.UserId}/searched-topics/{command.TopicId}");

            var searchedTopicDataDto = await GetSearchedTopicDataDtoAsync(command.UserId);

            var removeSearchedTopicDataCommand = new RemoveSearchedTopicDataCommand(UserId: command.UserId,
                SearchedTopicDataId: searchedTopicDataDto.SearchedTopicDataId);

            return removeSearchedTopicDataCommand;
        }

        public RemoveSearchedTopicDataTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if SearchedTopicData value object that has values like the ones that are
        //specified in RemoveSearchedTopicDataCommand is deleted from the database.
        [Fact]
        public async Task Remove_Searched_TopicData_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var command = await PrepareRemoveSearchedTopicDataCommandAsync();

            //ACT
            var response = await Delete($"/publichistory-service/users/{command.UserId}/searched-topics/{command.SearchedTopicDataId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted SearchedTopicData value object is not found in the database.
        [Fact]
        public async Task Remove_Searched_TopicData_Endpoint_Should_Remove_SearchedTopicData_From_The_Database()
        {
            //ARRANGE
            var command = await PrepareRemoveSearchedTopicDataCommandAsync();

            await Delete($"/publichistory-service/users/{command.UserId}/searched-topics/{command.SearchedTopicDataId}");

            //ACT
            var response = await Get($"/publichistory-service/users/{command.UserId}/searched-topics/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
