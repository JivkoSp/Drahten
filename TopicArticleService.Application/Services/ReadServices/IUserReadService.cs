
namespace TopicArticleService.Application.Services.ReadServices
{
    public interface IUserReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
