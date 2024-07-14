using Newtonsoft.Json;
using Shouldly;
using System.Net;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Presentation.Dtos;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RegisterArticleCommentDislikeTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private AddArticleCommentCommand AddArticleCommentCommand;

        //*** IMPORTANT *** - The CreateArticleCommand must have valid TopicId (TopicId for Topic that already exist in the database).

        private async Task<AddArticleCommentCommand> PrepareAddArticleCommentCommandAsync()
        {
            var createArticleCommand = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST",
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("e0e68a89-8cb2-4602-a10b-2be1a78a9be5"));

            await Post(createArticleCommand, "/topic-article-service/articles");

            var registerUserCommand = new RegisterUserCommand(Guid.NewGuid());

            await Post(registerUserCommand, "/topic-article-service/users");

            var addArticleCommentCommand = new AddArticleCommentCommand(createArticleCommand.ArticleId, Guid.NewGuid(),
                "TEST comment", DateTimeOffset.Now, registerUserCommand.UserId, null);

            return addArticleCommentCommand;
        }

        private async Task<AddArticleCommentDislikeCommand> PrepareAddArticleCommentDislikeCommandAsync()
        {
            AddArticleCommentCommand = await PrepareAddArticleCommentCommandAsync();

            await Post(AddArticleCommentCommand, $"/topic-article-service/articles/{AddArticleCommentCommand.ArticleId}/comments/");

            var addArticleCommentDislikeCommand = new AddArticleCommentDislikeCommand(AddArticleCommentCommand.ArticleCommentId,
                DateTimeOffset.Now, AddArticleCommentCommand.UserId);

            return addArticleCommentDislikeCommand;
        }

        public RegisterArticleCommentDislikeTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if ArticleCommentDislike is created from the specified parameters
        //in AddArticleCommentDislikeCommand.
        [Fact]
        public async Task Register_ArticleCommentDislike_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var addArticleCommentDislikeCommand = await PrepareAddArticleCommentDislikeCommandAsync();

            //ACT
            var response = await Post(addArticleCommentDislikeCommand,
               $"/topic-article-service/comments/{addArticleCommentDislikeCommand.ArticleCommentId}/dislikes/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/topic-article-service/comments/{addArticleCommentDislikeCommand.ArticleCommentId}/dislikes/");
        }

        //Should return http status code 200 when the previously created ArticleCommentDislike is successfully received from the database.
        [Fact]
        public async Task Register_ArticleCommentDislike_Endpoint_Should_Add_ArticleCommentDislike_With_Given_ArticleCommentId_And_UserId_To_The_Database()
        {
            //ARRANGE
            var addArticleCommentDislikeCommand = await PrepareAddArticleCommentDislikeCommandAsync();

            await Post(addArticleCommentDislikeCommand,
               $"/topic-article-service/comments/{addArticleCommentDislikeCommand.ArticleCommentId}/dislikes/");

            //ACT
            var response = await Get($"/topic-article-service/articles/{AddArticleCommentCommand.ArticleId.ToString("N")}/comments/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<ArticleCommentDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var articleCommentDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            //Comparing the values of the addArticleCommentDislikeCommand object that is send to the
            // /topic-article-service/articles/comments/{ArticleCommentId}/dislikes/ POST endpoint with the values of the
            // articleCommentDto object that is received by the /topic-article-service/articles/{ArticleId}/comments/ GET endpoint.
            //-----------------------
            //Within the articleCommentDto object, there is an ArticleCommentDislike object that is associated with the articleComment
            //and identified by {ArticleCommentId}.
            //The values of this ArticleCommentDislike object must correspond to the values of the addArticleCommentDislikeCommand object.

            articleCommentDto.ArticleCommentId.ShouldBe(addArticleCommentDislikeCommand.ArticleCommentId);

            var articleCommentDislike = articleCommentDto.ArticleCommentDislikeDtos.FirstOrDefault();

            articleCommentDislike.ShouldNotBeNull();

            articleCommentDislike.UserId.ShouldBe(addArticleCommentDislikeCommand.UserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((addArticleCommentDislikeCommand.DateTime - articleCommentDislike.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();

            //*** IMPORTANT - Time comparison ***
        }
    }
}
