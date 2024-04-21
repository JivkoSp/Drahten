
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class UserArticleReadModel
    {
        //Composite primary key { UserId, ArticleId }
        public string UserId { get; set; }
        public string ArticleId { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
        public virtual ArticleReadModel Article { get; set; }
    }
}
