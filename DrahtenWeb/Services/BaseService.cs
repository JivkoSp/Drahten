using DrahtenWeb.Dtos;
using DrahtenWeb.Exceptions;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using Newtonsoft.Json;
using Polly.Retry;
using Polly;
using System.Net.Http.Headers;
using System.Text;
using System.Net;

namespace DrahtenWeb.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        public ResponseDto Response { get; set; } = new ResponseDto();

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            // Define the retry policy
            _retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError ||
                                                                         r.StatusCode == HttpStatusCode.ServiceUnavailable ||
                                                                         r.StatusCode == HttpStatusCode.BadGateway)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (result, timeSpan, retryCount, context) =>
                    {
                        // TODO: Log the retry attempt
                        if (context.TryGetValue("apiRequest", out var requestObj) && requestObj is ApiRequest apiRequest)
                        {
                            Console.WriteLine($"Retry {retryCount} for {apiRequest.Url} after {timeSpan.Seconds} " +
                                $"seconds delay due to {result.Result.StatusCode}.");
                        }
                        else
                        {
                            Console.WriteLine($"Retry {retryCount} after {timeSpan.Seconds} seconds due to {result.Result.StatusCode}.");
                        }
                    });
        }

        #region DESCRIPTION
        // <summary>
        /// Creates an <see cref="HttpRequestMessage"/> based on the specified <paramref name="apiRequest"/>.
        /// </summary>
        /// <param name="apiRequest">The <see cref="ApiRequest"/> object containing the details for the HTTP request.</param>
        /// <returns>A new instance of <see cref="HttpRequestMessage"/> configured with the data from <paramref name="apiRequest"/>.</returns>
        #endregion
        private HttpRequestMessage CreateHttpRequestMessage(ApiRequest apiRequest)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(apiRequest.Url),
                Headers = { { "Accept", "application/json" } }
            };

            // Check if the request has data.
            if (apiRequest.Data != null)
            {
                // The request HAS data, set the contents of the HTTP message.
                httpRequestMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8, "application/json");
            }

            // Check the TYPE of the request.
            switch (apiRequest.ApiType)
            {
                case ApiType.GET:
                    httpRequestMessage.Method = HttpMethod.Get;
                    break;
                case ApiType.POST:
                    httpRequestMessage.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    httpRequestMessage.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    httpRequestMessage.Method = HttpMethod.Delete;
                    break;
            }

            return httpRequestMessage;
        }

        #region DESCRIPTION
        /// <summary>
        /// Sends an HTTP request based on the specified <paramref name="apiRequest"/> using the provided <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="apiRequest">The <see cref="ApiRequest"/> object containing the details for the HTTP request.</param>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance used to send the request.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is the <see cref="HttpResponseMessage"/> from the request.
        /// </returns>
        #endregion
        private async Task<HttpResponseMessage> SendHttpRequestAsync(ApiRequest apiRequest, HttpClient httpClient)
        {
            var httpRequestMessage = CreateHttpRequestMessage(apiRequest);
            return await httpClient.SendAsync(httpRequestMessage);
        }

        #region DESCRIPTION
        /// <summary>
        /// Sends an HTTP request based on the specified <paramref name="apiRequest"/> and asynchronously retrieves the response.
        /// </summary>
        /// <typeparam name="T">The type of the response object that this method will return.</typeparam>
        /// <param name="apiRequest">
        /// An <see cref="ApiRequest"/> object representing the HTTP request, including request type, URL, data, and access token.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is the response deserialized into an object of type <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="UrlNotFoundException">Thrown when the URL in <paramref name="apiRequest"/> is null or empty.</exception>
        #endregion
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            if (string.IsNullOrEmpty(apiRequest.Url))
            {
                throw new UrlNotFoundException();
            }

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Clear();

            // Check if the request has access token.
            if (!string.IsNullOrEmpty(apiRequest.AccessToken))
            {
                // The request HAS access token.
                // Put the access token in the authorization header of the request.
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
            }

            try
            {
                // Execute the HTTP request within the retry policy
               // var httpResponse = await _retryPolicy.ExecuteAsync(() => SendHttpRequestAsync(apiRequest, httpClient));

                var httpResponse = await _retryPolicy.ExecuteAsync(context =>
                {
                    // Capture apiRequest in the context to use it in the retry policy's logging
                    return SendHttpRequestAsync(apiRequest, httpClient);
                }, new Dictionary<string, object> { { "apiRequest", apiRequest } });


                // Initialize the variable "httpResponseSerializedContent" with the CONTENT OF THE HTTP RESPONSE MESSAGE.
                // This is json serialized string.
                var httpResponseSerializedContent = await httpResponse.Content.ReadAsStringAsync();

                // Initialize the variable "httpResponseDeserializedContent" with the deserialized content
                // of "httpResponseSerializedContent".
                var httpResponseDeserializedContent = JsonConvert.DeserializeObject<T>(httpResponseSerializedContent);

                return httpResponseDeserializedContent;
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"HttpRequestException occurred: {ex.Message}");
                throw;
            }
        }
    }
}
