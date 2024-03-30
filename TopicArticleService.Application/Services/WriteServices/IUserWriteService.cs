using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Application.Services.WriteServices
{
    public interface IUserWriteService
    {
        Task AddUserAsync(Guid userId);
    }
}
