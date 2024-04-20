using Newtonsoft.Json;
using System.Text;
using UserService.Application.Commands;
using UserService.Tests.EndToEnd.Factories;
using Xunit;

namespace UserService.Tests.EndToEnd.Sync
{
    public abstract class BaseSyncIntegrationTest : IClassFixture<UserServiceApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        private static StringContent GetContent(object value)
           => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

        public BaseSyncIntegrationTest(UserServiceApplicationFactory factory)
        {
            factory.Server.AllowSynchronousIO = true;
            _httpClient = factory.CreateClient();
        }

        protected Task<HttpResponseMessage> Get(string requestUri)
            => _httpClient.GetAsync(requestUri);

        protected Task<HttpResponseMessage> Post<TCommand>(TCommand command, string requestUri)
            where TCommand : ICommand
            => _httpClient.PostAsync(requestUri, GetContent(command));

        protected Task<HttpResponseMessage> Put<TCommand>(TCommand command, string requestUri)
            where TCommand : ICommand
             => _httpClient.PutAsync(requestUri, GetContent(command));

        protected Task<HttpResponseMessage> Delete(string requestUri)
             => _httpClient.DeleteAsync(requestUri);
    }
}
