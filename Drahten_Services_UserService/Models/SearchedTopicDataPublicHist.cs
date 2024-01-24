namespace Drahten_Services_UserService.Models
{
    public class SearchedTopicDataPublicHist
    {
        //Private key
        public int SearchedTopicDataPublicHistId { get; set; }
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }

        //Relationships
        public int TopicId { get; set; }
        public int PublicHistoryId { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual PublicHistory? PublicHistory { get; set; }
    }
}
