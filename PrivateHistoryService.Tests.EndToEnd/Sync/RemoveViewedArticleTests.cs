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
    public class RemoveViewedArticleTests : BaseSyncIntegrationTest
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

        private async Task<ViewedArticleDto> GetViewedArticleDtoAsync(Guid userId)
        {
            var viewedArticlesResponse = await Get($"/privatehistory-service/users/{userId}/viewed-articles/");

            var responseSerializedContent = await viewedArticlesResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            var responseResult = JsonConvert.DeserializeObject<List<ViewedArticleDto>>(Convert.ToString(responseDto.Result));

            return responseResult.FirstOrDefault();
        }

        private async Task<RemoveViewedArticleCommand> PrepareRemoveViewedArticleCommandAsync()
        {
            var command = await PrepareAddViewedArticleCommandAsync();

            await Post(command, $"/privatehistory-service/users/{command.UserId}/viewed-articles/{command.ArticleId}");

            var viewedArticleDto = await GetViewedArticleDtoAsync(command.UserId);

            var removeViewedArticleCommand = new RemoveViewedArticleCommand(UserId: command.UserId,
                ViewedArticleId: viewedArticleDto.ViewedArticleId);

            return removeViewedArticleCommand;
        }

        public RemoveViewedArticleTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if ViewedArticle value object that has values like the ones that are
        //specified in RemoveViewedArticleCommand is deleted from the database.
        [Fact]
        public async Task Remove_Viewed_Article_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var command = await PrepareRemoveViewedArticleCommandAsync();

            //ACT
            var response = await Delete($"/privatehistory-service/users/{command.UserId}/viewed-articles/{command.ViewedArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted ViewedArticle value object is not found in the database.
        [Fact]
        public async Task Remove_Viewed_Article_Endpoint_Should_Remove_ViewedArticle_From_The_Database()
        {
            //ARRANGE
            var command = await PrepareRemoveViewedArticleCommandAsync();

            await Delete($"/privatehistory-service/users/{command.UserId}/viewed-articles/{command.ViewedArticleId}");

            //ACT
            var response = await Get($"/privatehistory-service/users/{command.UserId}/viewed-articles/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
