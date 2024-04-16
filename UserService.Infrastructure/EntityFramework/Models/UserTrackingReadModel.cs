
namespace UserService.Infrastructure.EntityFramework.Models
{
    internal class UserTrackingReadModel
    {
        //Primary key
        public Guid UserTrackingId { get; set; }
        public string Action { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Referrer { get; set; }

        //Relationships
        public string UserId { get; set; }
        public virtual UserReadModel User { get; set; }
    }
}
