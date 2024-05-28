
namespace PrivateHistoryService.Application.Services.ReadServices
{
    public interface IUserReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
