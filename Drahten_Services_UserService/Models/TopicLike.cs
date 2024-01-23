namespace Drahten_Services_UserService.Models
{
    public class TopicLike
    {
        //Primary key
        public int TopicLikeId { get; set; }
        public DateTime DateTime { get; set; }

        //Relationships
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? User { get; set; }
    }
}
