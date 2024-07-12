using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetTopicsRelatedToUserQuery(Guid UserId) : IQuery<List<UserTopicDto>>;
}
