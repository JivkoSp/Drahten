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
    public class AddCommentedArticleTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddCommentedArticleCommand> PrepareAddCommentedArticleCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/publichistory-service/users/{userId}");

            var addCommentedArticleCommand = new AddCommentedArticleCommand(ArticleId: Guid.NewGuid(), UserId: userId,
                ArticleComment: "...", DateTime: DateTimeOffset.Now);

            return addCommentedArticleCommand;
        }

        public AddCommentedArticleTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if CommentedArticle value object is created from the specified parameters in AddCommentedArticleCommand.
        [Fact]
        public async Task Add_Commented_Article_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddCommentedArticleCommandAsync();

            //ACT
            var response = await Post(command,
                $"/publichistory-service/users/{command.UserId}/commented-articles/{command.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/publichistory-service/users/{command.UserId}/commented-articles/{command.ArticleId}");
        }

        //Should return http status code 200 when the previously created CommentedArticle value object is successfully received from the database.
        [Fact]
        public async Task Add_Commented_Article_Endpoint_Should_Add_CommentedArticle_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddCommentedArticleCommandAsync();

            //ACT
            await Post(command, $"/publichistory-service/users/{command.UserId}/commented-articles/{command.ArticleId}");

            var commentedArticlesResponse = await Get($"/publichistory-service/users/{command.UserId}/commented-articles/");

            //ASSERT
            commentedArticlesResponse.ShouldNotBeNull();

            commentedArticlesResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await commentedArticlesResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<CommentedArticleDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var commentedArticleDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            // The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            // This is because the ArticleId from the database is not expected to have hyphens (or dashes).
            commentedArticleDto.ArticleId.ShouldBe(command.ArticleId.ToString("N"));

            commentedArticleDto.UserId.ShouldBe(command.UserId.ToString());

            commentedArticleDto.ArticleComment.ShouldBe(command.ArticleComment);

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - commentedArticleDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
