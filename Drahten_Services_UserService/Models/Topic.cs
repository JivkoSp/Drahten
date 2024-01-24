namespace Drahten_Services_UserService.Models
{
    public class Topic
    {
        //Primary key
        public int TopicId { get; set; }
        public string TopicName { get; set; } = string.Empty;

        //Relationships
        public virtual HashSet<UserTopic>? Users { get; set; }
        public virtual HashSet<SearchedTopicDataPrivateHist>? SearchedTopicDataPrivateHist { get; set; }
        public virtual HashSet<SearchedTopicDataPublicHist>? SearchedTopicDataPublicHist { get; set; }
    }
}
