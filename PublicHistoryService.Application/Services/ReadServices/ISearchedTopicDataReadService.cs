using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Services.ReadServices
{
    public interface ISearchedTopicDataReadService
    {
        Task<SearchedTopicDataDto> GetSearchedTopicDataByIdAsync(Guid searchedTopicDataId);
    }
}
