
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class ArticleCommentLikeReadModel
    {
        //Composite primary key { ArticleCommentId, UserId }
        public Guid ArticleCommentId { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }

        //Relationships
        public virtual User User { get; set; }
        public virtual ArticleCommentReadModel ArticleComment { get; set; }
    }
}
