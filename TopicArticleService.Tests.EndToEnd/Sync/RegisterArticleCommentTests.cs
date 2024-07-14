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
    public class RegisterArticleCommentTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

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

        public RegisterArticleCommentTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //*** IMPORTANT *** - The CreateArticleCommand must have valid TopicId (TopicId for Topic that already exist in the database).

        //Should return http status code 201 if ArticleComment is created from the specified parameters in AddArticleCommentCommand.
        [Fact]
        public async Task Register_ArticleComment_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var addArticleCommentCommand = await PrepareAddArticleCommentCommandAsync();

            //ACT
            var response = await Post(addArticleCommentCommand,
                $"/topic-article-service/articles/{addArticleCommentCommand.ArticleId}/comments/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe($"/topic-article-service/articles/{addArticleCommentCommand.ArticleId}/comments/");
        }

        //Should return http status code 200 when the previously created ArticleComment is successfully received from the database.
        [Fact]
        public async Task Register_ArticleComment_Endpoint_Should_Add_ArticleComment_With_Given_ArticleCommentId_To_The_Database()
        {
            //ARRANGE
            var addArticleCommentCommand = await PrepareAddArticleCommentCommandAsync();

            await Post(addArticleCommentCommand, $"/topic-article-service/articles/{addArticleCommentCommand.ArticleId}/comments/");

            //ACT
            var response = await Get($"/topic-article-service/articles/{addArticleCommentCommand.ArticleId.ToString("N")}/comments/");

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

            //Comparing the values of the addArticleCommentCommand object that is send to the
            // /topic-article-service/articles/{ArticleId}/comments/ POST endpoint with the values of the ArticleCommentDto object
            //that is received by the /topic-article-service/articles/{ArticleId}/comments/ GET endpoint.

            articleCommentDto.ArticleDto.ArticleId.ShouldBe(addArticleCommentCommand.ArticleId.ToString("N"));

            articleCommentDto.ArticleCommentId.ShouldBe(addArticleCommentCommand.ArticleCommentId);

            articleCommentDto.CommentValue.ShouldBe(addArticleCommentCommand.CommentValue);

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.
      
            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((addArticleCommentCommand.DateTime - articleCommentDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();

            //*** IMPORTANT - Time comparison ***

            articleCommentDto.UserId.ShouldBe(addArticleCommentCommand.UserId.ToString());

            articleCommentDto.ParentArticleCommentId.ShouldBe(addArticleCommentCommand.ParentArticleCommentId);
        }
    }
}
