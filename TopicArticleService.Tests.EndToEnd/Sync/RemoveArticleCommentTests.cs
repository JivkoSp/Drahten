using Shouldly;
using System.Net;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RemoveArticleCommentTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        //*** IMPORTANT *** - The CreateArticleCommand must have valid TopicId (TopicId for Topic that already exist in the database).

        private async Task<AddArticleCommentCommand> PrepareAddArticleCommentCommandAsync()
        {
            var createArticleCommand = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST",
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("888a5c96-7c7c-4f98-90b6-91a0c2d401b0"));

            await Post(createArticleCommand, "/topic-article-service/articles");

            var registerUserCommand = new RegisterUserCommand(Guid.NewGuid());

            await Post(registerUserCommand, "/topic-article-service/users");

            var addArticleCommentCommand = new AddArticleCommentCommand(createArticleCommand.ArticleId, Guid.NewGuid(),
                "TEST comment", DateTimeOffset.Now.ToUtc(), registerUserCommand.UserId, null);

            return addArticleCommentCommand;
        }

        private async Task<RemoveArticleCommentCommand> PrepareRemoveArticleCommentCommandAsync()
        {
            var addArticleCommentCommand = await PrepareAddArticleCommentCommandAsync();

            await Post(addArticleCommentCommand, $"/topic-article-service/articles/{addArticleCommentCommand.ArticleId}/comments/");

            var removeArticleCommentCommand = new RemoveArticleCommentCommand(addArticleCommentCommand.ArticleId, 
                addArticleCommentCommand.ArticleCommentId);

            return removeArticleCommentCommand;
        }

        public RemoveArticleCommentTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if ArticleComment that has parameters like the ones that are
        //specified in AddArticleCommentCommand is deleted from the database.
        [Fact]
        public async Task Remove_ArticleComment_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var removeArticleCommentCommand = await PrepareRemoveArticleCommentCommandAsync();

            //ACT
            var response = await Delete($"/topic-article-service/articles/{removeArticleCommentCommand.ArticleId}/comments/{removeArticleCommentCommand.ArticleCommentId}");

            //ASSERT
            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted ArticleComment is not found in the database.
        [Fact]
        public async Task Remove_ArticleComment_Endpoint_Should_Remove_ArticleComment_From_The_Database()
        {
            //ARRANGE
            var removeArticleCommentCommand = await PrepareRemoveArticleCommentCommandAsync();

            await Delete($"/topic-article-service/articles/{removeArticleCommentCommand.ArticleId}/comments/{removeArticleCommentCommand.ArticleCommentId}");

            //ACT
            var response = await Get($"/topic-article-service/articles/{removeArticleCommentCommand.ArticleId}/comments/");

            //ASSERT
            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
