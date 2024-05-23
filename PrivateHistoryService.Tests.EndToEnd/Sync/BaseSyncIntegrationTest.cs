using Newtonsoft.Json;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Tests.EndToEnd.Factories;
using System.Text;
using Xunit;

namespace PrivateHistoryService.Tests.EndToEnd.Sync
{
    public abstract class BaseSyncIntegrationTest : IClassFixture<PrivateHistoryServiceApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public BaseSyncIntegrationTest(PrivateHistoryServiceApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _httpClient = factory.CreateClient();
        }

        private static StringContent GetContent(object value)
           => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

        protected Task<HttpResponseMessage> Get(string requestUri)
            => _httpClient.GetAsync(requestUri);

        protected Task<HttpResponseMessage> Post<TCommand>(TCommand command, string requestUri)
            where TCommand : ICommand
            => _httpClient.PostAsync(requestUri, GetContent(command));

        protected Task<HttpResponseMessage> Delete(string requestUri)
             => _httpClient.DeleteAsync(requestUri);
    }
}
