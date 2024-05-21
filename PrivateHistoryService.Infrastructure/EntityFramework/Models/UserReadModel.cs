
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class UserReadModel
    {
        //Primary key
        public string UserId { get; set; }
        public int Version { get; set; }

        //Relationships
        public virtual ICollection<ViewedArticleReadModel> ViewedArticles { get; set; }
        public virtual ICollection<TopicSubscriptionReadModel> TopicSubscriptions { get; set; }
        public virtual ICollection<SearchedArticleDataReadModel> SearchedArticles { get; set; }
        public virtual ICollection<SearchedTopicDataReadModel> SearchedTopics { get; set; }
        public virtual ICollection<CommentedArticleReadModel> CommentedArticles { get; set; }
        public virtual ICollection<LikedArticleReadModel> LikedArticles { get; set; }
        public virtual ICollection<DislikedArticleReadModel> DislikedArticles { get; set; }
        public virtual ICollection<LikedArticleCommentReadModel> LikedArticleComments { get; set; }
        public virtual ICollection<DislikedArticleCommentReadModel> DislikedArticleComments { get; set; }
        public virtual ICollection<ViewedUserReadModel> ViewedUsers { get; set; }
    }
}
