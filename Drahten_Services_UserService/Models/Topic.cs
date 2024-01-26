namespace Drahten_Services_UserService.Models
{
    public class Topic
    {
        //Primary key
        public int TopicId { get; set; }
        public string TopicName { get; set; } = string.Empty;

        //Relationships
        //Self relationship [one-to-many], represents hierarchy of topics - START
        public int? ParentTopicId { get; set; }
        public virtual Topic? Parent { get; set; }
        public virtual ICollection<Topic>? Children { get; set; }
        //Self relationship [one-to-many], represents hierarchy of topics - END
        public virtual HashSet<UserTopic>? Users { get; set; }
        public virtual HashSet<SearchedTopicDataPrivateHist>? SearchedTopicDataPrivateHist { get; set; }
        public virtual HashSet<SearchedTopicDataPublicHist>? SearchedTopicDataPublicHist { get; set; }
    }
}
