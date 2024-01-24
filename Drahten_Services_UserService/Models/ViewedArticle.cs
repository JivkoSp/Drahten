namespace Drahten_Services_UserService.Models
{
    public class ViewedArticle
    {
        //Primary and foreign key
        public int ArticleId { get; set; }
        public DateTime ViewTime { get; set; }

        //Relationships
        public int UserId { get; set; }
        public int PrivateHistoryId { get; set; }
        public virtual Article? Article { get; set; }
        public virtual User? User { get; set; }
        public virtual PrivateHistory? PrivateHistory { get; set; }
    }
}
