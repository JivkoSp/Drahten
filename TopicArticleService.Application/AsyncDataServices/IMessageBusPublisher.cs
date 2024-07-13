using TopicArticleService.Application.Dtos.PrivateHistoryService;

namespace TopicArticleService.Application.AsyncDataServices
{
    public interface IMessageBusPublisher
    {
        Task PublishViewedArticleAsync(ViewedArticleDto viewedArticleDto);
        Task PublishLikedArticleAsync(LikedArticleDto likedArticleDto);
        Task PublishDislikedArticleAsync(DislikedArticleDto dislikedArticleDto);
        Task PublishCommentedArticleAsync(CommentedArticleDto commentedArticleDto);
        Task PublishLikedArticleCommentAsync(LikedArticleCommentDto likedArticleCommentDto);
        Task PublishDislikedArticleCommentAsync(DislikedArticleCommentDto dislikedArticleCommentDto);
        Task PublishTopicSubscriptionAsync(TopicSubscriptionDto topicSubscriptionDto);
    }
}
