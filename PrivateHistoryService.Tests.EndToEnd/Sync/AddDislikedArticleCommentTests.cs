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
    public class AddDislikedArticleCommentTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddDislikedArticleCommentCommand> PrepareAddDislikedArticleCommentCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/privatehistory-service/users/{userId}");

            var addDislikedArticleCommentCommand = new AddDislikedArticleCommentCommand(ArticleId: Guid.NewGuid(), UserId: userId,
                ArticleCommentId: Guid.NewGuid(), DateTime: DateTimeOffset.Now);

            return addDislikedArticleCommentCommand;
        }

        public AddDislikedArticleCommentTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if DislikedArticleComment value object is created from the specified parameters
        //in AddDislikedArticleCommentCommand.
        [Fact]
        public async Task Add_Disliked_Article_Comment_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddDislikedArticleCommentCommandAsync();

            //ACT
            var response = await Post(command,
                $"/privatehistory-service/users/{command.UserId}/disliked-article-comments/{command.ArticleCommentId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/privatehistory-service/users/{command.UserId}/disliked-article-comments/{command.ArticleCommentId}");
        }

        //Should return http status code 200 when the previously created DislikedArticleComment value object is successfully received from the database.
        [Fact]
        public async Task Add_Disliked_Article_Comment_Endpoint_Should_Add_DislikedArticleComment_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddDislikedArticleCommentCommandAsync();

            //ACT
            await Post(command,
                $"/privatehistory-service/users/{command.UserId}/disliked-article-comments/{command.ArticleCommentId}");

            var dislikedArticleCommentsResponse = await Get($"/privatehistory-service/users/{command.UserId}/disliked-article-comments/");

            //ASSERT
            dislikedArticleCommentsResponse.ShouldNotBeNull();

            dislikedArticleCommentsResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await dislikedArticleCommentsResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<DislikedArticleCommentDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var dislikedArticleCommentDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            // The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            // This is because the ArticleId from the database is not expected to have hyphens (or dashes).
            dislikedArticleCommentDto.ArticleId.ShouldBe(command.ArticleId.ToString("N"));

            dislikedArticleCommentDto.UserId.ShouldBe(command.UserId.ToString());

            dislikedArticleCommentDto.ArticleCommentId.ShouldBe(command.ArticleCommentId);

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - dislikedArticleCommentDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
