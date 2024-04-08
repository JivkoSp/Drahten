using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Text;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Queries;
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

        private Task<HttpResponseMessage> Act(CreateArticleCommand command, string requestUri)
            => _httpClient.PostAsync(requestUri, GetContent(command));

        //private Task<HttpResponseMessage> GetData(string requestUri)
        //    => _httpClient.GetAsync(requestUri);

        [Fact]
        public async Task Register_Article_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST", 
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("fcf49b1c-e8d1-4134-a4cd-9ee01fac7383"));

            //ACT
            var response = await Act(command, "/topic-article-service/articles");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe("/topic-article-service/articles");


            //response = await GetData($"/topic-article-service/articles/{command.ArticleId}");

            //response.ShouldNotBeNull();
        }
    }
}
