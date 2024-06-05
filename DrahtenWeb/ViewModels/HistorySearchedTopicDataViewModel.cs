using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistorySearchedTopicDataViewModel
    {
        public List<SearchedTopicDataDto> SearchedTopics { get; set; }
        public Pagination Pagination { get; set; }
    }
}
