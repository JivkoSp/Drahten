using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetArticleLikesQuery : IQuery<List<ArticleLikeDto>>
    {
        public string ArticleId { get; set; }
    }
}
