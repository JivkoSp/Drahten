
namespace PublicHistoryService.Infrastructure.EntityFramework.Models
{
    internal class UserReadModel
    {
        //Primary key
        public string UserId { get; set; }
        public int Version { get; set; }

        //Relationships
        public virtual ICollection<ViewedArticleReadModel> ViewedArticles { get; set; }
        public virtual ICollection<ViewedUserReadModel> ViewedUsers { get; set; }
        public virtual ICollection<SearchedArticleDataReadModel> SearchedArticles { get; set; }
        public virtual ICollection<SearchedTopicDataReadModel> SearchedTopics { get; set; }
        public virtual ICollection<CommentedArticleReadModel> CommentedArticles { get; set; }
    }
}
