namespace Drahten_Services_UserService.Models
{
    public class UserTopic
    {
        //Composite primary key { UserId, TopicId }
        public string UserId { get; set; } = string.Empty;
        public int TopicId { get; set; }
        public DateTime SubscriptionTime { get; set; }

        //Relationships
        public virtual User? User { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}
