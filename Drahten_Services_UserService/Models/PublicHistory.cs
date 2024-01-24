namespace Drahten_Services_UserService.Models
{
    public class PublicHistory
    {
        //Primary and foreign key
        public int UserId { get; set; }

        //Relationships
        public virtual User? User { get; set; }
        public virtual ICollection<ViewedArticlePublicHist>? ViewedArticles { get; set; }
        public virtual ICollection<ViewedUserPublicHist>? ViewedUsers { get; set; }
        public virtual ICollection<SearchedTopicDataPublicHist>? SearchedTopicData { get; set; }
    }
}
