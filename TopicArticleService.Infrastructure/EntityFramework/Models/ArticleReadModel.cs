
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class ArticleReadModel
    {
        //Primary key
        public string ArticleId { get; set; }
        public int Version { get; set; }
        public string PrevTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishingDate { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }

        //Relationships
        public Guid TopicId { get; set; }
        public virtual TopicReadModel Topic { get; set; }
        public virtual ICollection<UserArticleReadModel> UserArticles { get; set; }
        public virtual ICollection<ArticleCommentReadModel> ArticleComments { get; set; }
        public virtual ICollection<ArticleLikeReadModel> ArticleLikes { get; set; }
        public virtual ICollection<ArticleDislikeReadModel> ArticleDislikes { get; set; }
    }
}
