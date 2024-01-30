namespace Drahten_Services_UserService.Models
{
    public class PublicHistory
    {
        //Primary and foreign key
        public string UserId { get; set; } = string.Empty;

        //Relationships
        public virtual User? User { get; set; }
        public virtual ICollection<ViewedArticlePublicHist>? ViewedArticles { get; set; }
        public virtual ICollection<ViewedUserPublicHist>? ViewedUsers { get; set; }
        public virtual ICollection<SearchedTopicDataPublicHist>? SearchedTopicData { get; set; }
    }
}
