
namespace TopicArticleService.Application.Services.ReadServices
{
    public interface ITopicReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
