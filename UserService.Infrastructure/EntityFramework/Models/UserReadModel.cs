
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
        public virtual ICollection<BannedUserReadModel> IssuedBansByUser { get; set; }
        public virtual ICollection<BannedUserReadModel> ReceivedBansForUser { get; set; }
        public virtual ICollection<ContactRequestReadModel> IssuedContactRequests { get; set; }
        public virtual ICollection<ContactRequestReadModel> ReceivedContactRequests { get; set; }
        public virtual ICollection<UserTrackingReadModel> AuditTrail { get; set; }
    }
}
