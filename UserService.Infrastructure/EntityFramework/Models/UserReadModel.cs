
namespace UserService.Infrastructure.EntityFramework.Models
{
    internal class UserReadModel
    {
        //Primary key
        public string UserId { get; set; }
        public int Version { get; set; }
        public string UserFullName { get; set; }
        public string UserNickName { get; set; }
        public string UserEmailAddress { get; set; }

        //Relationships
        public virtual ICollection<BannedUserReadModel> BannedUsers { get; set; }
        public virtual ICollection<ContactRequestReadModel> ContactRequests { get; set; }
        public virtual ICollection<UserTrackingReadModel> AuditTrail { get; set; }
    }
}
