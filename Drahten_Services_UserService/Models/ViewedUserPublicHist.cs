namespace Drahten_Services_UserService.Models
{
    public class ViewedUserPublicHist
    {
        //Primary and foreign key 
        public string ViewedUserId { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }

        //Relationships
        public string PublicHistoryId { get; set; } = string.Empty;
        public virtual User? User { get; set; }
        public virtual PublicHistory? PublicHistory { get; set; }
    }
}
