namespace Drahten_Services_UserService.Models
{
    public class ViewedUserPrivateHist
    {
        //Primary and foreign key 
        public string ViewedUserId { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }

        //Relationships
        public string PrivateHistoryId { get; set; } = string.Empty;
        public virtual User? User { get; set; } 
        public virtual PrivateHistory? PrivateHistory { get; set; }
    }
}
