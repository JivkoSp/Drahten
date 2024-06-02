using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Services.ReadServices
{
    public interface ISearchedTopicDataReadService
    {
        Task<SearchedTopicDataDto> GetSearchedTopicDataByIdAsync(Guid searchedTopicDataId);
    }
}
