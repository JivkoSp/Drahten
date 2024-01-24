namespace Drahten_Services_UserService.Models
{
    public class ArticleLike
    {
        //Primary key
        public int ArticleLikeId { get; set; }
        public DateTime DateTime { get; set; }

        //Relationships
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public virtual Article? Article { get; set; }
        public virtual User? User { get; set; }
    }
}
