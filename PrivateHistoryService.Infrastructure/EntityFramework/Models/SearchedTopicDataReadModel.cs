
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class SearchedTopicDataReadModel
    {
        //Primary key
        public Guid SearchedTopicDataId { get; set; }
        public Guid TopicId { get; set; }
        public string UserId { get; set; }
        public string SearchedData { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
    }
}
