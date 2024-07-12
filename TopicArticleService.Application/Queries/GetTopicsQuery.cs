using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetTopicsQuery : IQuery<List<TopicDto>>;
}
