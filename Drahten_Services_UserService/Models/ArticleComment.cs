namespace Drahten_Services_UserService.Models
{
    public class ArticleComment
    {
        //Primary key
        public int ArticleCommentId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }

        //Relationships
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public virtual Article? Article { get; set; }
        public virtual User? User { get; set; }
    }
}
