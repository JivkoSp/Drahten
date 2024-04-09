using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Shouldly;
using System.Net;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RegisterArticleLikeTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        //*** IMPORTANT *** - The CreateArticleCommand must have valid TopicId (TopicId for Topic that already exist in the database).

        private async Task<AddArticleLikeCommand> PrepareAddArticleLikeCommandAsync()
        {
            var createArticleCommand = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST",
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("888a5c96-7c7c-4f98-90b6-91a0c2d401b0"));

            await Post(createArticleCommand, "/topic-article-service/articles");

            var registerUserCommand = new RegisterUserCommand(Guid.NewGuid());

            await Post(registerUserCommand, "/topic-article-service/users");

            var addArticleLikeCommand = new AddArticleLikeCommand(createArticleCommand.ArticleId,
                DateTimeOffset.Now.ToUtc(), registerUserCommand.UserId);

            return addArticleLikeCommand;
        }

        public RegisterArticleLikeTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if ArticleLike is created from the specified parameters in AddArticleLikeCommand.
        [Fact]
        public async Task Register_ArticleLike_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var addArticleLikeCommand = await PrepareAddArticleLikeCommandAsync();

            //ACT
            var response = await Post(addArticleLikeCommand,
                $"/topic-article-service/articles/{addArticleLikeCommand.ArticleId}/likes/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe($"/topic-article-service/articles/{addArticleLikeCommand.ArticleId}/likes/");
        }

        //Should return http status code 200 when the previously created ArticleLike is successfully received from the database.
        [Fact]
        public async Task Register_ArticleLike_Endpoint_Should_Add_ArticleLike_With_Given_ArticleId_And_UserId_To_The_Database()
        {
            //ARRANGE
            var addArticleLikeCommand = await PrepareAddArticleLikeCommandAsync();

            await Post(addArticleLikeCommand, $"/topic-article-service/articles/{addArticleLikeCommand.ArticleId}/likes/");

            //ACT
            var response = await Get($"/topic-article-service/articles/{addArticleLikeCommand.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var articleDto = JsonConvert.DeserializeObject<ArticleDto>(responseSerializedContent);

            //Comparing the values of the addArticleLikeCommand object that is send to the
            // /topic-article-service/articles/{ArticleId}/likes/ POST endpoint with the values of the ArticleDto object
            //that is received by the /topic-article-service/articles/{ArticleId} GET endpoint.
            //-----------------------
            //Within the ArticleDto object, there is an ArticleLike object that is associated with the article
            //and identified by {ArticleId}.
            //The values of this ArticleLike object must correspond to the values of the addArticleLikeCommand object.

            articleDto.ArticleId.ShouldBe(addArticleLikeCommand.ArticleId.ToString());

            var articleLike = articleDto.ArticleLikeDtos.FirstOrDefault();

            articleLike.UserId.ShouldBe(addArticleLikeCommand.UserId);

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((addArticleLikeCommand.DateTime - articleLike.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();

            //*** IMPORTANT - Time comparison ***
        }
    }
}
