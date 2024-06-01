using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Services.ReadServices
{
    public interface ICommentedArticleReadService
    {
        Task<CommentedArticleDto> GetCommentedArticleByIdAsync(Guid commentedArticleId);
    }
}
