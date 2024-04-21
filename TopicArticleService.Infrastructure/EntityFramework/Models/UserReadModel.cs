
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class UserReadModel
    {
        //Primary key
        public string UserId { get; set; }
        public int Version { get; set; }

        //Relationships
        public virtual HashSet<UserTopicReadModel> Topics { get; set; }
        public virtual HashSet<UserArticleReadModel> UserArticles { get; set; }
        public virtual HashSet<ArticleCommentReadModel> ArticleComments { get; set; }
        public virtual HashSet<ArticleLikeReadModel> ArticleLikes { get; set; }
        public virtual HashSet<ArticleDislikeReadModel> ArticleDislikes { get; set; }
        public virtual HashSet<ArticleCommentLikeReadModel> ArticleCommentLikes { get; set; }
        public virtual HashSet<ArticleCommentDislikeReadModel> ArticleCommentDislikes { get; set; }
    }
}
