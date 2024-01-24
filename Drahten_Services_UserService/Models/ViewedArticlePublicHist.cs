namespace Drahten_Services_UserService.Models
{
    public class ViewedArticlePublicHist
    {
        //Primary and foreign key
        public int ArticleId { get; set; }
        public DateTime ViewTime { get; set; }

        //Relationships
        public int PublicHistoryId { get; set; }
        public virtual Article? Article { get; set; }
        public virtual PublicHistory? PublicHistory { get; set; }
    }
}
