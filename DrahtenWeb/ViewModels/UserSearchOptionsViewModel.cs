using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class UserSearchOptionsViewModel
    {
        public List<ArticleDto> Articles { get; set; } = new List<ArticleDto>();
        public List<TopicDto> Topics { get; set; } = new List<TopicDto>();
        public Dictionary<string, List<ReadArticleCommentDto>> ArticleComments { get; set; } = new Dictionary<string, List<ReadArticleCommentDto>>();
        public Dictionary<string, List<ReadUserArticleDto>> UsersRelatedToArticle { get; set; } = new Dictionary<string, List<ReadUserArticleDto>>();
        public Dictionary<Guid, List<ArticleDto>> TopicArticles { get; set; } = new Dictionary<Guid, List<ArticleDto>>();
        public Dictionary<Guid, List<UserTopicDto>> TopicSubscriptions { get; set; } = new Dictionary<Guid, List<UserTopicDto>>();
        public Dictionary<Guid, HashSet<KeyValuePair<string, string>>> TopicSources { get; set; } = new Dictionary<Guid, HashSet<KeyValuePair<string, string>>>();
        public List<UserTopicDto> UserTopics { get; set; } = new List<UserTopicDto>();
    }
}
