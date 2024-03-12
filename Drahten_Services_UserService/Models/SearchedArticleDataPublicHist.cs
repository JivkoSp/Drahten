namespace Drahten_Services_UserService.Models
{
    public class SearchedArticleDataPublicHist
    {
        //Primary key
        public int SearchedArticleDataPublicHistId { get; set; }
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }

        //Relationships
        public string ArticleId { get; set; } = string.Empty;
        public string PublicHistoryId { get; set; } = string.Empty;
        public virtual Article? Article { get; set; }
        public virtual PublicHistory? PublicHistory { get; set; }
    }
}
