
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class ArticleLikeReadModel
    {
        //Composite primary key { ArticleId, UserId }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }

        //Relationships
        public virtual ArticleReadModel Article { get; set; }
        public virtual User User { get; set; }
    }
}
