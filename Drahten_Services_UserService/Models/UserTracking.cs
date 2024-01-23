namespace Drahten_Services_UserService.Models
{
    public class UserTracking
    {
        //Primary key
        public int UserTrackingId { get; set; }

        //Relationships
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
