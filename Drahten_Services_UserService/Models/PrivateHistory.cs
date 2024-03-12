namespace Drahten_Services_UserService.Models
{
    public class PrivateHistory
    {
        //Primary and foreign key
        public string UserId { get; set; } = string.Empty;
        //Represent the amount of time that the user's private history
        //will be available (after this time the history is deleted).
        public DateTime HistoryLiveTime { get; set; }

        //Relationships
        public virtual User? User { get; set; }
        public virtual ICollection<ViewedArticlePrivateHist>? ViewedArticles { get; set; }
        public virtual ICollection<SearchedArticleDataPrivateHist>? SearchedArticleData { get; set; }
        public virtual ICollection<ViewedUserPrivateHist>? ViewedUsers { get; set; }
        public virtual ICollection<SearchedTopicDataPrivateHist>? SearchedTopicData { get; set; }
    }
}
