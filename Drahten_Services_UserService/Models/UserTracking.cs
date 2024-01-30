namespace Drahten_Services_UserService.Models
{
    public class UserTracking
    {
        //Primary key
        public int UserTrackingId { get; set; }

        //Relationships
        public string UserId { get; set; } = string.Empty;
        public virtual User? User { get; set; }
    }
}
