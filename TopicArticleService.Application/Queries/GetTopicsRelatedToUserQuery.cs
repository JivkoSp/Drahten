using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetTopicsRelatedToUserQuery : IQuery<List<UserTopicDto>>
    {
        public Guid UserId { get; set; }
    }
}
