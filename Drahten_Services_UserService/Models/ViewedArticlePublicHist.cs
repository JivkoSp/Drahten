namespace Drahten_Services_UserService.Models
{
    public class ViewedArticlePublicHist
    {
        //Primary and foreign key
        public string ArticleId { get; set; } = string.Empty;
        public DateTime ViewTime { get; set; }

        //Relationships
        public string PublicHistoryId { get; set; } = string.Empty;
        public virtual Article? Article { get; set; }
        public virtual PublicHistory? PublicHistory { get; set; }
    }
}
