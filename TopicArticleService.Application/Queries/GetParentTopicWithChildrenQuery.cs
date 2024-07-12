using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetParentTopicWithChildrenQuery(Guid TopicId) : IQuery<TopicDto>;
}
