using System.Collections.ObjectModel;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Entities
{
    public class User : AggregateRoot<UserID>
    {
        private List<UserTopic> _subscribedTopics = new List<UserTopic>();

        public IReadOnlyCollection<UserTopic> SubscribedTopics
        {
            get { return new ReadOnlyCollection<UserTopic>(_subscribedTopics); }
        }

        private User() {}

        internal User(UserID userId)
        {
            ValidateConstructorParameters<NullUserParametersException>([userId]);

            Id = userId;
        }
        
        public void SubscribeToTopic(UserTopic userTopic)
        {
            var alreadyExists = _subscribedTopics.Any(x => x.TopicId == userTopic.TopicId);    

            if (alreadyExists)
            {
                throw new UserTopicAlreadyExistsException(userTopic.UserId, userTopic.TopicId);
            }

            _subscribedTopics.Add(userTopic);

            AddEvent(new UserTopicAdded(this, userTopic));
        }
    }
}
