namespace DrahtenWeb.Services.IServices
{
    public interface IUserService : IBaseService
    {
        Task<TEntity> GetUserTopics<TEntity>(string accessToken);
    }
}
