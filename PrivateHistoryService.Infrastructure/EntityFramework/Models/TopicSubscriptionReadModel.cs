
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class TopicSubscriptionReadModel
    {
        //Composite primary key { TopicId, UserId }
        public Guid TopicId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
    }
}
