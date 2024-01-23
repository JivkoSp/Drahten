using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;

namespace DrahtenWeb.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) 
        {
           
        }

        //The purpose of this method is to test the api gateway.
        //TOOD: Must be removed after the testing is done.
        public async Task<T> GetEndpointAsync<T>(string accessToken)
        {
            var response = await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = "https://localhost:7076/api/user/",
                AccessToken = accessToken
            });

            return response;
        }
    }
}
