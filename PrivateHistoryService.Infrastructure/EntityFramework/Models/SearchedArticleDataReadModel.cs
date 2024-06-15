
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class SearchedArticleDataReadModel
    {
        //Primary key
        public Guid SearchedArticleDataId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string SearchedData { get; set; }
        public string SearchedDataAnswer { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
    }
}
