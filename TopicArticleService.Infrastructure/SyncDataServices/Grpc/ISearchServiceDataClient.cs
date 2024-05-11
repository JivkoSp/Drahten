using TopicArticleService.Application.Dtos.SearchService;

namespace TopicArticleService.Infrastructure.SyncDataServices.Grpc
{
    internal interface ISearchServiceDataClient
    {
        IAsyncEnumerable<(string, Document)> GetDocumentsAsync();
        IAsyncEnumerable<double> DocumentSimilarityCheckAsync(DocumentDto documentDto);
    }
}
