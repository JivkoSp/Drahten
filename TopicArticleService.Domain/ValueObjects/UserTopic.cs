using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record UserTopic
    {
        public UserID UserId { get; }
        public TopicId TopicId { get; }
        internal DateTimeOffset SubscriptionTime { get; }

        private UserTopic() {}

        public UserTopic(UserID userId, TopicId topicId, DateTimeOffset dateTime)
        {
            if (userId == null)
            {
                throw new NullUserIdException();
            }

            if(topicId == null)
            {
                throw new NullTopicIdException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidUserTopicDateTimeException();
            }

            UserId = userId;
            TopicId = topicId;
            SubscriptionTime = dateTime;
        }
    }
}
