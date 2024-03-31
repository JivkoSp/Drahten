
namespace TopicArticleService.Application.Dtos
{
    public class ArticleCommentDto
    {
        public Guid ArticleCommentId { get; set; }
        public string CommentValue { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public Guid ParentArticleCommentId { get; set; }
        public ArticleDto ArticleDto { get; set; }
        public List<ArticleCommentDto> Children { get; set; }
        public List<ArticleCommentLikeDto> ArticleCommentLikeDtos { get; set; }
        public List<ArticleCommentDislikeDto> ArticleCommentDislikeDtos { get; set; }
    }
}
