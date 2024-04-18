
namespace UserService.Infrastructure.EntityFramework.Models
{
    internal class ContactRequestReadModel
    {
        //Composite primary key { IssuerUserId, ReceiverUserId }
        public string IssuerUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset DateTime {  get; set; }
        
        //Relationships
        public virtual UserReadModel Issuer { get; set; }
        public virtual UserReadModel Receiver { get; set; }
    }
}
