using Newtonsoft.Json;
using Shouldly;
using System.Net;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RegisterArticleTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        public RegisterArticleTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //*** IMPORTANT *** - The CreateArticleCommand must have valid TopicId (TopicId for Topic that already exist in the database).


        //Should return http status code 201 if Article is created from the specified parameters in CreateArticleCommand.
        [Fact]
        public async Task Register_Article_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST", 
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("888a5c96-7c7c-4f98-90b6-91a0c2d401b0"));

            //ACT
            var response = await Post(command, "/topic-article-service/articles");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe("/topic-article-service/articles");
        }

        //Should return http status code 200 when the previously created Article is successfully received from the database.
        [Fact]
        public async Task Register_Article_Endpoint_Should_Add_Article_With_Given_ArticleId_To_The_Database()
        {
            //ARRANGE
            var command = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST",
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("888a5c96-7c7c-4f98-90b6-91a0c2d401b0"));

            //ACT
            await Post(command, "/topic-article-service/articles");

            var response = await Get($"/topic-article-service/articles/{command.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var articleDto = JsonConvert.DeserializeObject<ArticleDto>(responseSerializedContent);

            //Comparing the values of the command object that is send to the /topic-article-service/articles POST endpoint
            //with the values of the articleDto object that is received by the /topic-article-service/articles/{ArticleId} GET endpoint.

            articleDto.ArticleId.ShouldBe(command.ArticleId.ToString());

            articleDto.PrevTitle.ShouldBe(command.PrevTitle);

            articleDto.Title.ShouldBe(command.Title);

            articleDto.Content.ShouldBe(command.Content);

            articleDto.PublishingDate.ShouldBe(command.PublishingDate);

            articleDto.Author.ShouldBe(command.Author);

            articleDto.Link.ShouldBe(command.Link);

            articleDto.TopicId.ShouldBe(command.TopicId);  
        }
    }
}
