
namespace TopicArticleService.Application.Dtos
{
    public class ArticleCommentDto
    {
        public string CommentValue { get; set; }
        public DateTime DateTime { get; set; }
        public Guid UserId { get; set; }
        public Guid ParentArticleCommentId { get; set; }
        public List<ArticleCommentDto> Children { get; set; }
        public List<ArticleCommentLikeDto> ArticleCommentLikeDtos { get; set; }
        public List<ArticleCommentDislikeDto> ArticleCommentDislikeDtos { get; set; }
    }
}
