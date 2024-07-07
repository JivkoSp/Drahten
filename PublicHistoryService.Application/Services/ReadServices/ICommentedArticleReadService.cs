using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Services.ReadServices
{
    public interface ICommentedArticleReadService
    {
        Task<CommentedArticleDto> GetCommentedArticleByIdAsync(Guid commentedArticleId);
    }
}
