
namespace UserService.Infrastructure.EntityFramework.Models
{
    internal class BannedUserReadModel
    {
        //Composite primary key { IssuerUserId, ReceiverUserId }
        public string IssuerUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel Issuer {  get; set; } 
        public virtual UserReadModel Receiver { get; set; }
    }
}
