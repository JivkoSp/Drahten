namespace Drahten_Services_UserService.Models
{
    public class ArticleCommentThumbsDown
    {
        //Composite primary key { ArticleCommentId, UserId }
        public int ArticleCommentId { get; set; }
        public string UserId { get; set; } = string.Empty;

        //Relationships
        public virtual User? User { get; set; }
        public virtual ArticleComment? ArticleComment { get; set; }
    }
}
