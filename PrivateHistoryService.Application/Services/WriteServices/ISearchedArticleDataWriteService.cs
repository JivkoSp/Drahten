using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface ISearchedArticleDataWriteService
    {
        Task AddSearchedArticleDataAsync(SearchedArticleData searchedArticleData);
    }
}
