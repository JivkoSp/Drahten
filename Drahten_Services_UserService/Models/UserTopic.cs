namespace Drahten_Services_UserService.Models
{
    public class UserTopic
    {
        //Composite primary key { UserId, TopicId }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public DateTime SubscriptionTime { get; set; }

        //Relationships
        public virtual User? User { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}
