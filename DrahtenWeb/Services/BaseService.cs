using DrahtenWeb.Dtos;
using DrahtenWeb.Exceptions;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DrahtenWeb.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ResponseDto Response { get; set; } = new ResponseDto();

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Method that sends http request.
        /// </summary>
        /// <typeparam name="T">Represents the type of response that this method will generate.</typeparam>
        /// <input><param name="apiRequest">
        /// Object that represents http reguest, includes: Type, Url, Data, AccessToken.
        /// </param></input> 
        /// <returns>Task<T></returns>
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            if (string.IsNullOrEmpty(apiRequest.Url))
            {
                throw new UrlNotFoundException();
            }

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Clear();

            //Check if the request has accesstoken.
            if (!string.IsNullOrEmpty(apiRequest.AccessToken))
            {
                //The request HAS accesstoken.

                //Put the accesstoken in the authorization header of the request.
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
            }

            var httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.Headers.Add("Accept", "application/json");

            httpRequestMessage.RequestUri = new Uri(apiRequest.Url);

            //Check if the request has data.
            if (apiRequest.Data != null)
            {
                //The request HAS data, set the contents of the http message.
                httpRequestMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8, "application/json");
            }

            //Check the TYPE of the request.
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

            //Send the http request and initialize the variable "httpResponse" with object of type HttpResponseMessage,
            //containing the CONTENT OF THE HTTP RESPONSE MESSAGE, STATUS CODE and other information that relates to the request.
            var httpResponse = await httpClient.SendAsync(httpRequestMessage);

            //Initialize the variable "httpResponseSerializedContent" with the CONTENT OF THE HTTP RESPONSE MESSAGE.
            //This is json serialized string.
            var httpResponseSerializedContent = await httpResponse.Content.ReadAsStringAsync();

            //Initialize the variable "httpResponseDeserializedContent" with the deserialized content
            //of "httpResponseSerializedContent".
            var httpResponseDeserializedContent = JsonConvert.DeserializeObject<T>(httpResponseSerializedContent);

            return httpResponseDeserializedContent;
        }
    }
}
