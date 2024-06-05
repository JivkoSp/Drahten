using TopicArticleService.Application.Dtos.PrivateHistoryService;

namespace TopicArticleService.Infrastructure.AsyncDataServices
{
    public interface IMessageBusPublisher
    {
        void PublishViewedArticle(ViewedArticleDto viewedArticleDto);
    }
}
