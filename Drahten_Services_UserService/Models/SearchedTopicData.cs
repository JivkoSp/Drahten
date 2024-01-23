namespace Drahten_Services_UserService.Models
{
    public class SearchedTopicData
    {
        //Private key
        public int SearchedTopicDataId { get; set; }
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }

        //Relationships
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public int PrivateHistoryId { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? User { get; set; }
        public virtual PrivateHistory? PrivateHistory { get; set; }
    }
}
