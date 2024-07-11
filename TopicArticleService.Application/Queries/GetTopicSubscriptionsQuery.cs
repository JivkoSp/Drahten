using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetTopicSubscriptionsQuery : IQuery<List<UserTopicDto>>
    {
        public Guid TopicId { get; set; }
    }
}
