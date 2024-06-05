using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryTopicSubscriptionsViewModel
    {
        public List<TopicSubscriptionDto> TopicSubscriptions { get; set; }
        public Pagination Pagination { get; set; }
    }
}
