
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class ViewedArticleReadModel
    {
        //Primary key
        public Guid ViewedArticleId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
    }
}
