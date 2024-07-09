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
    public class AddSearchedArticleDataTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddSearchedArticleDataCommand> PrepareAddSearchedArticleDataCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/publichistory-service/users/{userId}");

            var addSearchedArticleDataCommand = new AddSearchedArticleDataCommand(ArticleId: Guid.NewGuid(), UserId: userId,
                SearchedData: "...", DateTime: DateTimeOffset.Now);

            return addSearchedArticleDataCommand;
        }

        public AddSearchedArticleDataTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if SearchedArticleData value object is created from the specified parameters in AddSearchedArticleDataCommand.
        [Fact]
        public async Task Add_Searched_Article_Data_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddSearchedArticleDataCommandAsync();

            //ACT
            var response = await Post(command,
                $"/publichistory-service/users/{command.UserId}/searched-articles/{command.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/publichistory-service/users/{command.UserId}/searched-articles/{command.ArticleId}");
        }

        //Should return http status code 200 when the previously created SearchedArticleData value object is successfully received from the database.
        [Fact]
        public async Task Add_Searched_Article_Data_Endpoint_Should_Add_SearchedArticleData_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddSearchedArticleDataCommandAsync();

            //ACT
            await Post(command,
                 $"/publichistory-service/users/{command.UserId}/searched-articles/{command.ArticleId}");

            var searchedArticlesResponse = await Get($"/publichistory-service/users/{command.UserId}/searched-articles/");

            //ASSERT
            searchedArticlesResponse.ShouldNotBeNull();

            searchedArticlesResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await searchedArticlesResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<SearchedArticleDataDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var searchedArticleDataDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            // The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            // This is because the ArticleId from the database is not expected to have hyphens (or dashes).
            searchedArticleDataDto.ArticleId.ShouldBe(command.ArticleId.ToString("N"));

            searchedArticleDataDto.UserId.ShouldBe(command.UserId.ToString());

            searchedArticleDataDto.SearchedData.ShouldBe(command.SearchedData);

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - searchedArticleDataDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
