namespace Drahten_Services_UserService.Models
{
    public class ArticleComment
    {
        //Primary key
        public int ArticleCommentId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }

        //Relationships
        //Self relationship [one-to-many], represents hierarchy of comments - START
        public int? ParentArticleCommentId { get; set; }
        public virtual ArticleComment? Parent { get; set; }
        public virtual ICollection<ArticleComment>? Children { get; set; }
        //Self relationship [one-to-many], represents hierarchy of comments - END
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public virtual Article? Article { get; set; }
        public virtual User? User { get; set; }
        public virtual HashSet<ArticleCommentThumbsUp>? ArticleCommentThumbsUp { get; set; }
        public virtual HashSet<ArticleCommentThumbsDown>? ArticleCommentThumbsDown { get; set; }
    }
}
