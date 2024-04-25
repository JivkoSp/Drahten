namespace DrahtenWeb.Dtos.TopicArticleService
{
    public class ReadArticleCommentDto
    {
        public Guid ArticleCommentId { get; set; }
        public string CommentValue { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string UserId { get; set; }
        public Guid? ParentArticleCommentId { get; set; }
        public ArticleDto ArticleDto { get; set; }
        public List<ReadArticleCommentDto> Children { get; set; } = new List<ReadArticleCommentDto>();
        public List<ArticleCommentLikeDto> ArticleCommentLikeDtos { get; set; } = new List<ArticleCommentLikeDto>();
        public List<ArticleCommentDislikeDto> ArticleCommentDislikeDtos { get; set; } = new List<ArticleCommentDislikeDto>();
    }
}
