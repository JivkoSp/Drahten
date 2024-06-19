using AutoMapper;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TopicArticleService.Application.Dtos.SearchService;

namespace TopicArticleService.Infrastructure.SyncDataServices.Grpc
{
    internal sealed class SearchServiceDataClient : ISearchServiceDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchServiceDataClient> _logger;

        public SearchServiceDataClient(IConfiguration configuration, IMapper mapper, ILogger<SearchServiceDataClient> logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        //Returns all documents that are available in the SearchService.
        public async IAsyncEnumerable<(string, Document)> GetDocumentsAsync()
        {
            Console.WriteLine($"\n\n--> Calling the SearchService GRPC Server at address: {_configuration["GrpcSearchService"]}.\n\n");

            _logger.LogInformation($"--> Calling the SearchService GRPC Server at address: {_configuration["GrpcSearchService"]}.");

            using var channel = GrpcChannel.ForAddress(_configuration["GrpcSearchService"]);

            var client = new DocumentFinder.DocumentFinderClient(channel);

            var reply = client.FindDoucments(new FindDocumentsRequest { });

            while (await reply.ResponseStream.MoveNext(CancellationToken.None))
            {
                var currentResponse = reply.ResponseStream.Current;

                yield return (currentResponse.DocumentTopic, currentResponse.Document);
            }
        }

        public SimilarityScoreResponse DocumentSimilarityCheckAsync(DocumentDto documentDto)
        {
            Console.WriteLine($"\n\n--> Calling the SearchService GRPC Server at address: {_configuration["GrpcSearchService"]}.\n\n");

            _logger.LogInformation($"--> Calling the SearchService GRPC Server at address: {_configuration["GrpcSearchService"]}.");

            using var channel = GrpcChannel.ForAddress(_configuration["GrpcSearchService"]);

            var client = new DocumentSimilarityCheck.DocumentSimilarityCheckClient(channel);

            var document = _mapper.Map<Document>(documentDto);

            var reply = client.CheckDocumentSimilarity(document);

            return reply;
        }
    }
}
