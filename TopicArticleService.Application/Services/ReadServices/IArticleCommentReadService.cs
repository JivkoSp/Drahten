using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Services.ReadServices
{
    public interface IArticleCommentReadService
    {
        Task<ArticleCommentDto> GetArticleCommentByIdAsync(Guid articleCommentId);
    }
}
