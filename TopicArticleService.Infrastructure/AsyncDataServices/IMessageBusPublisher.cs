using TopicArticleService.Application.Dtos.SearchService;

namespace TopicArticleService.Infrastructure.AsyncDataServices
{
    public interface IMessageBusPublisher
    {
        void PublishNewDocument(DocumentDto documentDto);
    }
}
