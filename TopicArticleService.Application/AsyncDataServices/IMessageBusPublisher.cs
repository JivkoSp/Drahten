using TopicArticleService.Application.Dtos.PrivateHistoryService;

namespace TopicArticleService.Application.AsyncDataServices
{
    public interface IMessageBusPublisher
    {
        void PublishViewedArticle(ViewedArticleDto viewedArticleDto);
        void PublishLikedArticle(LikedArticleDto likedArticleDto);
        void PublishDislikedArticle(DislikedArticleDto dislikedArticleDto);
        void PublishCommentedArticle(CommentedArticleDto commentedArticleDto);
    }
}
