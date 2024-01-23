namespace DrahtenWeb.Services.IServices
{
    public interface IUserService : IBaseService
    {
        Task<T> GetEndpointAsync<T>(string accessToken);
    }
}
