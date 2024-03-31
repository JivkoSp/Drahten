using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetArticleCommentsQuery : IQuery<List<ArticleCommentDto>>
    {
        public string ArticleId { get; set; }
    }
}
