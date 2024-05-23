using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record SearchedTopicData
    {
        public TopicID TopicID { get; }
        public UserID UserID { get; }
        internal SearchedData SearchedData { get; }
        internal DateTimeOffset DateTime { get; }

        private SearchedTopicData() {}

        public SearchedTopicData(TopicID topicId, UserID userId, SearchedData searchedData, DateTimeOffset dateTime)
        {
            if (topicId == null)
            {
                throw new NullTopicIdException();
            }

            if (userId == null)
            {
                throw new NullUserIdException();
            }

            if (searchedData == null)
            {
                throw new NullSearchedDataException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidSearchedTopicDataDateTimeException();
            }

            TopicID = topicId;
            UserID = userId;
            SearchedData = searchedData;
            DateTime = dateTime;
        }
    }
}
