using DrahtenWeb.Dtos;

namespace DrahtenWeb.ViewModels
{
    public class UserSearchOptionsViewModel
    {
        public List<TopicDto> Topics { get; set; } = new List<TopicDto>();
        public List<ReadUserTopicDto> UserTopics { get; set; } = new List<ReadUserTopicDto>();
    }
}
