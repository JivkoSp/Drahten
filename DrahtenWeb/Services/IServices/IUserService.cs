using DrahtenWeb.Dtos.UserService;

namespace DrahtenWeb.Services.IServices
{
    public interface IUserService : IBaseService
    {
        Task<TEntity> GetUserByIdAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetIssuedBansByUserAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetReceivedBansByUserAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetIssuedContactRequestsByUserAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetReceivedContactRequestsByUserAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> RegisterUserAsync<TEntity>(UserDto userDto, string accessToken);
        Task<TEntity> RegisterBannedUserAsync<TEntity>(BanUserDto banUserDto, string accessToken);
        Task<TEntity> RegisterContactRequestAsync<TEntity>(ContactRequestDto contactRequestDto, string accessToken);
        Task<TEntity> RegisterUserActivityAsync<TEntity>(UserActivityDto userActivityDto, string accessToken);
        Task<TEntity> UpdateContactRequestMessageAsync<TEntity>(ContactRequestDto contactRequestDto, string accessToken);
        Task<TEntity> RemoveIssuedContactRequestAsync<TEntity>(Guid issuerUserId, Guid receiverUserId, string accessToken);
        Task<TEntity> RemoveReceivedContactRequestAsync<TEntity>(Guid receiverUserId, Guid issuerUserId, string accessToken);
        Task<TEntity> RemoveBannedUserAsync<TEntity>(Guid issuerUserId, Guid receiverUserId, string accessToken);
    }
}