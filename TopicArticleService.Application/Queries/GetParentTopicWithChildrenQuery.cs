using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetParentTopicWithChildrenQuery : IQuery<TopicDto>
    {
        public Guid TopicId { get; set; }
    }
}
