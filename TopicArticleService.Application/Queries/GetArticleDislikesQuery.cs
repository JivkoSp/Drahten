using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetArticleDislikesQuery : IQuery<List<ArticleDislikeDto>>
    {
        public string ArticleId { get; set; }
    }
}
