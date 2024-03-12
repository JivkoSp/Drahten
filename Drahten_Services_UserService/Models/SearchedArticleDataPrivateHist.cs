namespace Drahten_Services_UserService.Models
{
    public class SearchedArticleDataPrivateHist
    {
        //Primary key
        public int SearchedArticleDataPrivateHistId { get; set; }
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }

        //Relationships
        public string ArticleId { get; set; } = string.Empty;
        public string PrivateHistoryId { get; set; } = string.Empty;
        public virtual Article? Article { get; set; }
        public virtual PrivateHistory? PrivateHistory { get; set; }
    }
}
