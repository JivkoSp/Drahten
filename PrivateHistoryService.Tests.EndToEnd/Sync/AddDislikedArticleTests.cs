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
    public class AddDislikedArticleTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddDislikedArticleCommand> PrepareAddDislikedArticleCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/privatehistory-service/users/{userId}");

            var addDislikedArticleCommand = new AddDislikedArticleCommand(ArticleId: Guid.NewGuid(), UserId: userId,
                DateTime: DateTimeOffset.Now);

            return addDislikedArticleCommand;
        }

        public AddDislikedArticleTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if DislikedArticle value object is created from the specified parameters in AddDislikedArticleCommand.
        [Fact]
        public async Task Add_Disliked_Article_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddDislikedArticleCommandAsync();

            //ACT
            var response = await Post(command,
                $"/privatehistory-service/users/{command.UserId}/disliked-articles/{command.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/privatehistory-service/users/{command.UserId}/disliked-articles/{command.ArticleId}");
        }

        //Should return http status code 200 when the previously created DislikedArticle value object is successfully received from the database.
        [Fact]
        public async Task Add_Disliked_Article_Endpoint_Should_Add_DislikedArticle_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddDislikedArticleCommandAsync();

            //ACT
            await Post(command, $"/privatehistory-service/users/{command.UserId}/disliked-articles/{command.ArticleId}");

            var dislikedArticlesResponse = await Get($"/privatehistory-service/users/{command.UserId}/disliked-articles/");

            //ASSERT
            dislikedArticlesResponse.ShouldNotBeNull();

            dislikedArticlesResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await dislikedArticlesResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<DislikedArticleDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var dislikedArticleDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            // The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            // This is because the ArticleId from the database is not expected to have hyphens (or dashes).
            dislikedArticleDto.ArticleId.ShouldBe(command.ArticleId.ToString("N"));

            dislikedArticleDto.UserId.ShouldBe(command.UserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - dislikedArticleDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
