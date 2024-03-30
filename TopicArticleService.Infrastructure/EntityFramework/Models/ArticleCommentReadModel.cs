
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class ArticleCommentReadModel
    {
        //Primary key
        public Guid ArticleCommentId { get; set; }
        public int Version { get; set; }
        public string Comment { get; set; } 
        public DateTime DateTime { get; set; }

        //Relationships
        //Self relationship [one-to-many], represents hierarchy of comments - START
        public Guid? ParentArticleCommentId { get; set; }
        public virtual ArticleCommentReadModel Parent { get; set; }
        public virtual ICollection<ArticleCommentReadModel> Children { get; set; }
        //Self relationship [one-to-many], represents hierarchy of comments - END
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public virtual ArticleReadModel Article { get; set; }
        public virtual User User { get; set; }
        public virtual HashSet<ArticleCommentLikeReadModel> ArticleCommentLikes { get; set; }
        public virtual HashSet<ArticleCommentDislikeReadModel> ArticleCommentDislikes { get; set; }
    }
}
