using DrahtenWeb.Dtos;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;

namespace DrahtenWeb.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) 
        {
           
        }

        public async Task<TEntity> GetUserById<TEntity>(string userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest { 
            
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/users/{userId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterUser<TEntity>(WriteUserDto user, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest { 
                
                ApiType = ApiType.POST,
                Url = "https://localhost:7076/user-service/users/",
                Data = user,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterUserTopic<TEntity>(WriteUserDto user, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = "https://localhost:7076/user-service/topics/",
                Data = user,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetTopics<TEntity>(string accessToken)
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
