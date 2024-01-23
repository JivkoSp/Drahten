namespace Drahten_Services_UserService.Models
{
    public class UserArticle
    {
        //Composite primary key { UserId, ArticleId }
        public int UserId { get; set; }
        public int ArticleId { get; set; }

        //Relationships
        public virtual User? User { get; set; }
        public virtual Article? Article { get; set; }
    }
}
