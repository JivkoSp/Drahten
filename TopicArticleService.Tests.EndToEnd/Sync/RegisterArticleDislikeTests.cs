using Newtonsoft.Json;
using Shouldly;
using System.Net;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RegisterArticleDislikeTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        //*** IMPORTANT *** - The CreateArticleCommand must have valid TopicId (TopicId for Topic that already exist in the database).

        private async Task<AddArticleDislikeCommand> PrepareAddArticleDislikeCommandAsync()
        {
            var createArticleCommand = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST",
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("888a5c96-7c7c-4f98-90b6-91a0c2d401b0"));

            await Post(createArticleCommand, "/topic-article-service/articles");

            var registerUserCommand = new RegisterUserCommand(Guid.NewGuid());

            await Post(registerUserCommand, "/topic-article-service/users");

            var addArticleDislikeCommand = new AddArticleDislikeCommand(createArticleCommand.ArticleId,
                DateTimeOffset.Now.ToUtc(), registerUserCommand.UserId);

            return addArticleDislikeCommand;
        }

        public RegisterArticleDislikeTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if ArticleDislike is created from the specified parameters in AddArticleDislikeCommand.
        [Fact]
        public async Task Register_ArticleDislike_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var addArticleDislikeCommand = await PrepareAddArticleDislikeCommandAsync();

            //ACT
            var response = await Post(addArticleDislikeCommand,
                $"/topic-article-service/articles/{addArticleDislikeCommand.ArticleId}/dislikes/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/topic-article-service/articles/{addArticleDislikeCommand.ArticleId}/dislikes/");
        }

        //Should return http status code 200 when the previously created ArticleDislike is successfully received from the database.
        [Fact]
        public async Task Register_ArticleDislike_Endpoint_Should_Add_ArticleDislike_With_Given_ArticleId_And_UserId_To_The_Database()
        {
            //ARRANGE
            var addArticleDislikeCommand = await PrepareAddArticleDislikeCommandAsync();

            await Post(addArticleDislikeCommand, $"/topic-article-service/articles/{addArticleDislikeCommand.ArticleId}/dislikes/");

            //ACT
            var response = await Get($"/topic-article-service/articles/{addArticleDislikeCommand.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var articleDto = JsonConvert.DeserializeObject<ArticleDto>(responseSerializedContent);

            //Comparing the values of the addArticleDislikeCommand object that is send to the
            // /topic-article-service/articles/{ArticleId}/dislikes/ POST endpoint with the values of the ArticleDto object
            //that is received by the /topic-article-service/articles/{ArticleId} GET endpoint.
            //-----------------------
            //Within the ArticleDto object, there is an ArticleDislike object that is associated with the article
            //and identified by {ArticleId}.
            //The values of this ArticleDislike object must correspond to the values of the addArticleDislikeCommand object.

            articleDto.ArticleId.ShouldBe(addArticleDislikeCommand.ArticleId.ToString());

            var articleDislike = articleDto.ArticleDislikeDtos.FirstOrDefault();

            articleDislike.UserId.ShouldBe(addArticleDislikeCommand.UserId);

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((addArticleDislikeCommand.DateTime - articleDislike.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();

            //*** IMPORTANT - Time comparison ***
        }
    }
}
