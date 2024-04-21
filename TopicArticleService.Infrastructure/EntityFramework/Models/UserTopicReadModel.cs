
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class UserTopicReadModel
    {
        //Composite primary key { UserId, TopicId }
        public string UserId { get; set; }
        public Guid TopicId { get; set; }
        public DateTimeOffset SubscriptionTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
        public virtual TopicReadModel Topic { get; set; }
    }
}
