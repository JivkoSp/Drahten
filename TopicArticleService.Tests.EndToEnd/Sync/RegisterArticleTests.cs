using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Text;
using TopicArticleService.Application.Commands;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RegisterArticleTests : IClassFixture<DrahtenApplicationFactory>
    {
        #region GLOBAL ARRANGE

        private readonly HttpClient _httpClient;

        public RegisterArticleTests(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _httpClient = factory.CreateClient();
        }

        #endregion

        private static StringContent GetContent(object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

        private Task<HttpResponseMessage> Post(CreateArticleCommand command, string requestUri)
            => _httpClient.PostAsync(requestUri, GetContent(command));

        private Task<HttpResponseMessage> Get(string requestUri)
            => _httpClient.GetAsync(requestUri);

        //*** IMPORTANT *** - The CreateArticleCommand must have valid TopicId (TopicId for Topic that already exist in the database).


        //Should return http status code 201 if Article is created from the specified parameters in CreateArticleCommand.
        [Fact]
        public async Task Register_Article_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST", 
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("fcf49b1c-e8d1-4134-a4cd-9ee01fac7383"));

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
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("fcf49b1c-e8d1-4134-a4cd-9ee01fac7383"));

            //ACT
            await Post(command, "/topic-article-service/articles");

            var response = await Get($"/topic-article-service/articles/{command.ArticleId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var responseDeserializedContent = JsonConvert.DeserializeObject<CreateArticleCommand>(responseSerializedContent);

            //Comparing the values of the command object that is send to the /topic-article-service/articles POST endpoint
            //with the values of the response that is received by the /topic-article-service/articles/{ArticleId} GET endpoint.

            responseDeserializedContent.ArticleId.ShouldBe(command.ArticleId);

            responseDeserializedContent.PrevTitle.ShouldBe(command.PrevTitle);

            responseDeserializedContent.Title.ShouldBe(command.Title);

            responseDeserializedContent.Content.ShouldBe(command.Content);

            responseDeserializedContent.PublishingDate.ShouldBe(command.PublishingDate);

            responseDeserializedContent.Author.ShouldBe(command.Author);

            responseDeserializedContent.Link.ShouldBe(command.Link);

            responseDeserializedContent.TopicId.ShouldBe(command.TopicId);  
        }
    }
}
