using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetTopicSubscriptionsQuery(Guid TopicId) : IQuery<List<UserTopicDto>>;
}
