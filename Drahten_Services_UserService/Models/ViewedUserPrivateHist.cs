namespace Drahten_Services_UserService.Models
{
    public class ViewedUserPrivateHist
    {
        //Primary and foreign key 
        public int ViewedUserId { get; set; }
        public DateTime DateTime { get; set; }

        //Relationships
        public int PrivateHistoryId { get; set; }
        public virtual User? User { get; set; }
        public virtual PrivateHistory? PrivateHistory { get; set; }
    }
}
