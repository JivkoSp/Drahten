
namespace TopicArticleService.Infrastructure.EntityFramework.Models
{
    internal class TopicReadModel
    {
        //Primary key
        public Guid TopicId { get; set; }
        public int Version { get; set; }
        public string TopicName { get; set; }
        public string TopicFullName { get; set; }
        //Relationships
        //Self relationship [one-to-many], represents hierarchy of topics - START
        public Guid? ParentTopicId { get; set; }
        public virtual TopicReadModel Parent { get; set; }
        public virtual ICollection<TopicReadModel> Children { get; set; }
        //Self relationship [one-to-many], represents hierarchy of topics - END
        public virtual HashSet<UserTopicReadModel> Users { get; set; }
        public virtual HashSet<ArticleReadModel> Articles { get; set; }
    }
}
