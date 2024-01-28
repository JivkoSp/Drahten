using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;

namespace DrahtenWeb.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) 
        {
           
        }

        public async Task<TEntity> GetUserTopics<TEntity>(string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest {
            
                ApiType = ApiType.GET,
                Url = "https://localhost:7076/user-service/topics/",
                AccessToken = accessToken
            });

            return response;
        }
    }
}
