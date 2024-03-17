using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Entities
{
    public class Topic : AggregateRoot<TopicId>
    {
        private TopicName _topicName;
        private List<Topic> _topicChildren = new List<Topic>();

        public Topic(TopicId id, TopicName topicName)
        {
            Id = id;
            _topicName = topicName;
        }

        public void AddTopicChild(Topic topic)
        {
            var alreadyExists = _topicChildren.Any(x => x._topicName == topic._topicName);

            if (alreadyExists)
            {
                throw new TopicChildAlreadyExistsException(Id, topic._topicName);
            }

            _topicChildren.Add(topic);
        }
    }
}
