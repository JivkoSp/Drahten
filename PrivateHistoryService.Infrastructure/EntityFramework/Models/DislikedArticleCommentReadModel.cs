
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class DislikedArticleCommentReadModel
    {
        //Composite primary key { ArticleCommentId, UserId }
        public Guid ArticleCommentId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
    }
}
