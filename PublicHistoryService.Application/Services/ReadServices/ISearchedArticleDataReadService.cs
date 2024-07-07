using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Services.ReadServices
{
    public interface ISearchedArticleDataReadService
    {
        Task<SearchedArticleDataDto> GetSearchedArticleDataByIdAsync(Guid searchedArticleDataId);
    }
}
