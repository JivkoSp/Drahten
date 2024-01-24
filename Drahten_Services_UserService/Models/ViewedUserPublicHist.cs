namespace Drahten_Services_UserService.Models
{
    public class ViewedUserPublicHist
    {
        //Primary and foreign key 
        public int ViewedUserId { get; set; }
        public DateTime DateTime { get; set; }

        //Relationships
        public int PublicHistoryId { get; set; }
        public virtual User? User { get; set; }
        public virtual PublicHistory? PublicHistory { get; set; }
    }
}
