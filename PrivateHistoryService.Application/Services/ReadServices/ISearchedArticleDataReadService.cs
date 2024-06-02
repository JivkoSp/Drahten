using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Services.ReadServices
{
    public interface ISearchedArticleDataReadService
    {
        Task<SearchedArticleDataDto> GetSearchedArticleDataByIdAsync(Guid searchedArticleDataId);
    }
}
