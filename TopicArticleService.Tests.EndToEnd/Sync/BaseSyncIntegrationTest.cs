using Newtonsoft.Json;
using System.Text;
using TopicArticleService.Application.Commands;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public abstract class BaseSyncIntegrationTest : IClassFixture<DrahtenApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public BaseSyncIntegrationTest(DrahtenApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _httpClient = factory.CreateClient();
        }

        protected static StringContent GetContent(object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

        protected Task<HttpResponseMessage> Post<TCommand>(TCommand command, string requestUri)
            where TCommand : ICommand
            => _httpClient.PostAsync(requestUri, GetContent(command));

        protected Task<HttpResponseMessage> Get(string requestUri)
            => _httpClient.GetAsync(requestUri);
    }
}
