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
    public class RemoveCommentedArticleTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddCommentedArticleCommand> PrepareAddCommentedArticleCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/privatehistory-service/users/{userId}");

            var addCommentedArticleCommand = new AddCommentedArticleCommand(ArticleId: Guid.NewGuid(), UserId: userId,
                ArticleComment: "...", DateTime: DateTimeOffset.Now);

            return addCommentedArticleCommand;
        }

        private async Task<CommentedArticleDto> GetCommentedArticleDtoAsync(Guid userId)
        {
            var commentedArticlesResponse = await Get($"/privatehistory-service/users/{userId}/commented-articles/");

            var responseSerializedContent = await commentedArticlesResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            var responseResult = JsonConvert.DeserializeObject<List<CommentedArticleDto>>(Convert.ToString(responseDto.Result));

            return responseResult.FirstOrDefault();
        }

        private async Task<RemoveCommentedArticleCommand> PrepareRemoveCommentedArticleCommandAsync()
        {
            var command = await PrepareAddCommentedArticleCommandAsync();

            await Post(command, $"/privatehistory-service/users/{command.UserId}/commented-articles/{command.ArticleId}");

            var commentedArticleDto = await GetCommentedArticleDtoAsync(command.UserId);

            var removeCommentedArticleCommand = new RemoveCommentedArticleCommand(UserId: command.UserId, 
                CommentedArticleId: commentedArticleDto.CommentedArticleId);

            return removeCommentedArticleCommand;
        }

        public RemoveCommentedArticleTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if CommentedArticle value object that has values like the ones that are
        //specified in RemoveCommentedArticleCommand is deleted from the database.
        [Fact]
        public async Task Remove_Commented_Article_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var command = await PrepareRemoveCommentedArticleCommandAsync();

            //ACT
            var response = await Delete($"/privatehistory-service/users/{command.UserId}/commented-articles/{command.CommentedArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted CommentedArticle value object is not found in the database.
        [Fact]
        public async Task Remove_Commented_Article_Endpoint_Should_Remove_CommentedArticle_From_The_Database()
        {
            //ARRANGE
            var command = await PrepareRemoveCommentedArticleCommandAsync();

            await Delete($"/privatehistory-service/users/{command.UserId}/commented-articles/{command.CommentedArticleId}");

            //ACT
            var response = await Get($"/privatehistory-service/users/{command.UserId}/commented-articles/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
