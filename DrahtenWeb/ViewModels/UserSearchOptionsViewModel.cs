using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class UserSearchOptionsViewModel
    {
        public List<TopicDto> Topics { get; set; } = new List<TopicDto>();
        public List<UserTopicDto> UserTopics { get; set; } = new List<UserTopicDto>();
    }
}
