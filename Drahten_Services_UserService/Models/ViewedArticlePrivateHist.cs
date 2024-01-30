namespace Drahten_Services_UserService.Models
{
    public class ViewedArticlePrivateHist
    {
        //Primary and foreign key
        public string ArticleId { get; set; } = string.Empty;
        public DateTime ViewTime { get; set; }

        //Relationships
        public string PrivateHistoryId { get; set; } = string.Empty;
        public virtual Article? Article { get; set; }
        public virtual PrivateHistory? PrivateHistory { get; set; }
    }
}
