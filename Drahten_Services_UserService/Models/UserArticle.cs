namespace Drahten_Services_UserService.Models
{
    public class UserArticle
    {
        //Composite primary key { UserId, ArticleId }
        public string UserId { get; set; } = string.Empty;
        public string ArticleId { get; set; } = string.Empty;

        //Relationships
        public virtual User? User { get; set; }
        public virtual Article? Article { get; set; }
    }
}
