namespace Drahten_Services_UserService.Models
{
    public class SearchedTopicDataPrivateHist
    {
        //Private key
        public int SearchedTopicDataPrivateHistId { get; set; }
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }

        //Relationships
        public int TopicId { get; set; }
        public int PrivateHistoryId { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual PrivateHistory? PrivateHistory { get; set; }
    }
}
