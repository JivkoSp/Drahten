using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace TopicArticleService.Infrastructure.SyncDataServices.Grpc
{
    internal sealed class SearchServiceDataClient : ISearchServiceDataClient
    {
        private readonly IConfiguration _configuration;

        public SearchServiceDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Returns all documents that are available in the SearchService.
        public async IAsyncEnumerable<(string, Document)> GetDocumentsAsync()
        {
            Console.WriteLine($"\n\n--> Calling the SearchService GRPC Server at address: {_configuration["GrpcSearchService"]}.\n\n");

            using var channel = GrpcChannel.ForAddress(_configuration["GrpcSearchService"]);

            var client = new DocumentFinder.DocumentFinderClient(channel);

            var reply = client.FindDoucments(new FindDocumentsRequest { });

            while (await reply.ResponseStream.MoveNext(CancellationToken.None))
            {
                var currentResponse = reply.ResponseStream.Current;

                yield return (currentResponse.DocumentTopic, currentResponse.Document);
            }
        }
    }
}
