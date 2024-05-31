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
    public class AddLikedArticleTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddLikedArticleCommand> PrepareAddLikedArticleCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/privatehistory-service/users/{userId}");

            var addLikedArticleCommand = new AddLikedArticleCommand(ArticleId: Guid.NewGuid(), UserId: userId,
                DateTime: DateTimeOffset.Now);

            return addLikedArticleCommand;
        }

        public AddLikedArticleTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if LikedArticle value object is created from the specified parameters in AddLikedArticleCommand.
        [Fact]
        public async Task Add_Liked_Article_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddLikedArticleCommandAsync();

            //ACT
            var response = await Post(command,
                $"/privatehistory-service/users/{command.UserId}/liked-articles/{command.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/privatehistory-service/users/{command.UserId}/liked-articles/{command.ArticleId}");
        }

        //Should return http status code 200 when the previously created LikedArticle value object is successfully received from the database.
        [Fact]
        public async Task Add_Liked_Article_Endpoint_Should_Add_LikedArticle_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddLikedArticleCommandAsync();

            //ACT
            await Post(command, $"/privatehistory-service/users/{command.UserId}/liked-articles/{command.ArticleId}");

            var likedArticlesResponse = await Get($"/privatehistory-service/users/{command.UserId}/liked-articles/");

            //ASSERT
            likedArticlesResponse.ShouldNotBeNull();

            likedArticlesResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await likedArticlesResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<LikedArticleDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var likedArticleDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            // The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            // This is because the ArticleId from the database is not expected to have hyphens (or dashes).
            likedArticleDto.ArticleId.ShouldBe(command.ArticleId.ToString("N"));

            likedArticleDto.UserId.ShouldBe(command.UserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - likedArticleDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
