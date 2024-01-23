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
            //TODO: This method may have room for impovment.
            //      Possible refactoring of the method.

            try
            {
                if(string.IsNullOrEmpty(apiRequest.Url))
                {
                    throw new UrlNotFoundException();
                }

                var httpClient = _httpClientFactory.CreateClient();

                httpClient.DefaultRequestHeaders.Clear();

                //Check if the request has accesstoken.
                if(!string.IsNullOrEmpty(apiRequest.AccessToken))
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
                if(apiRequest.Data != null)
                {
                    //The request HAS data, set the contents of the http message.
                    httpRequestMessage.Content = new StringContent(
                        JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                //Check the TYPE of the request.
                switch(apiRequest.ApiType)
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

                if(httpResponseDeserializedContent == null)
                {
                    throw new HttpDeserializedContentNotFoundException();
                }

                return httpResponseDeserializedContent;
            }

            //TODO: Refactor the code in the catch blocks.
            //      Needs to be in dedicated method, becouse repetition occurs.

            catch(UrlNotFoundException ex)
            {
                //Print the exception message to the console for debbugging.
                //TDOD: Must be removed when the method is tested.
                Console.WriteLine(ex.Message);

                var responseDto = new ResponseDto
                {
                    ErrorMessages = new List<string>
                    {
                        Convert.ToString(ex.Message)    
                    }
                };

                //Serialize and Deserialize the "responseDto" object.
                //Reason: Becouse the return type is generic (type T) and will be known when the method is called. 

                var serializedResponseDto = JsonConvert.SerializeObject(responseDto);
                var deserializedResponseDto = JsonConvert.DeserializeObject<T>(serializedResponseDto);

                if(deserializedResponseDto == null)
                {
                    throw new ResponseDtoDeserializationException();
                }

                return deserializedResponseDto;
            }
            catch(HttpDeserializedContentNotFoundException ex)
            {
                //Print the exception message to the console for debbugging.
                //TDOD: Must be removed when the method is tested.
                Console.WriteLine(ex.Message);

                var responseDto = new ResponseDto
                {
                    ErrorMessages = new List<string>
                    {
                        Convert.ToString(ex.Message)
                    }
                };

                //Serialize and Deserialize the "responseDto" object.
                //Reason: Becouse the return type is generic (type T) and will be known when the method is called. 

                var serializedResponseDto = JsonConvert.SerializeObject(responseDto);
                var deserializedResponseDto = JsonConvert.DeserializeObject<T>(serializedResponseDto);

                if (deserializedResponseDto == null)
                {
                    throw new ResponseDtoDeserializationException();
                }

                return deserializedResponseDto;
            }
            catch (Exception ex) 
            {
                //Print the exception message to the console for debbugging.
                //TDOD: Must be removed when the method is tested.
                Console.WriteLine(ex.Message);

                var responseDto = new ResponseDto
                {
                    ErrorMessages = new List<string>
                    {
                        Convert.ToString(ex.Message)
                    }
                };

                //Serialize and Deserialize the "responseDto" object.
                //Reason: Becouse the return type is generic (type T) and will be known when the method is called. 

                var serializedResponseDto = JsonConvert.SerializeObject(responseDto);
                var deserializedResponseDto = JsonConvert.DeserializeObject<T>(serializedResponseDto);

                if (deserializedResponseDto == null)
                {
                    throw new ResponseDtoDeserializationException();
                }

                return deserializedResponseDto;
            }
        }
    }
}
