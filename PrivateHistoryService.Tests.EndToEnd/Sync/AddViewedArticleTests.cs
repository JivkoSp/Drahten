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
    public class AddViewedArticleTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddViewedArticleCommand> PrepareAddViewedArticleCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/privatehistory-service/users/{userId}");

            var addViewedArticleCommand = new AddViewedArticleCommand(ArticleId: Guid.NewGuid(), UserId: userId,
                DateTime: DateTimeOffset.Now);

            return addViewedArticleCommand;
        }

        public AddViewedArticleTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if ViewedArticle value object is created from the specified parameters in AddViewedArticleCommand.
        [Fact]
        public async Task Add_Viewed_Article_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddViewedArticleCommandAsync();

            //ACT
            var response = await Post(command,
                $"/privatehistory-service/users/{command.UserId}/viewed-articles/{command.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/privatehistory-service/users/{command.UserId}/viewed-articles/{command.ArticleId}");
        }

        //Should return http status code 200 when the previously created ViewedArticle value object is successfully received from the database.
        [Fact]
        public async Task Add_Viewed_Article_Endpoint_Should_Add_ViewedArticle_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddViewedArticleCommandAsync();

            //ACT
            await Post(command, $"/privatehistory-service/users/{command.UserId}/viewed-articles/{command.ArticleId}");

            var viewedArticlesResponse = await Get($"/privatehistory-service/users/{command.UserId}/viewed-articles/");

            //ASSERT
            viewedArticlesResponse.ShouldNotBeNull();

            viewedArticlesResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await viewedArticlesResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<ViewedArticleDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var viewedArticleDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            // The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            // This is because the ArticleId from the database is not expected to have hyphens (or dashes).
            viewedArticleDto.ArticleId.ShouldBe(command.ArticleId.ToString("N"));

            viewedArticleDto.UserId.ShouldBe(command.UserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - viewedArticleDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
