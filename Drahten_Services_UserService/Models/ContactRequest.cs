namespace Drahten_Services_UserService.Models
{
    public class ContactRequest
    {
        //Primary key
        public int ContactRequestId { get; set; }
        public string? Message { get; set; }
        public DateTime DateTime { get; set; }

        //Relationships
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public virtual User? Sender { get; set; }
        public virtual User? Receiver { get; set; }
    }
}
