
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class ArticleCommentDislikeReadModel
    {
        //Composite primary key { ArticleCommentId, UserId }
        public Guid ArticleCommentId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
        public virtual ArticleCommentReadModel ArticleComment { get; set; }
    }
}
