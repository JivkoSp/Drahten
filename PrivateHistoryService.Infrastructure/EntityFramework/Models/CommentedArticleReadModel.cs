
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class CommentedArticleReadModel
    {
        //Primary key
        public Guid CommentedArticleId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string ArticleComment { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
    }
}
