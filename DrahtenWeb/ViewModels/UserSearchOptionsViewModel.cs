using DrahtenWeb.Dtos;

namespace DrahtenWeb.ViewModels
{
    public class UserSearchOptionsViewModel
    {
        public List<ReadTopicDto> Topics { get; set; } = new List<ReadTopicDto>();
        public List<ReadUserTopicDto> UserTopics { get; set; } = new List<ReadUserTopicDto>();
    }
}
