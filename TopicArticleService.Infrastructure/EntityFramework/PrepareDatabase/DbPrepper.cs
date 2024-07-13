using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Infrastructure.Extensions;
using TopicArticleService.Infrastructure.SyncDataServices.Grpc;

namespace TopicArticleService.Infrastructure.EntityFramework.PrepareDatabase
{
    // Responsible for preparing and seeding the database with initial data. 
    internal sealed class DbPrepper : IHostedService
    {
        // Private field to hold the service provider for resolving dependencies.
        private readonly IServiceProvider _serviceProvider;

        public DbPrepper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider; 
        }

        private async Task SeedData(IArticleReadService articleReadService, ITopicReadService topicReadService, 
            IArticleRepository articleRepository, IArticleFactory articleFactory, IAsyncEnumerable<(string, Document)> documentTopic)
        {
            Console.WriteLine($"\n\n--> Seeding NEW documents from SearchService.\n\n");

            await foreach (var (topicName, document) in documentTopic)
            {
                var alreadyExists = await articleReadService.ExistsByIdAsync(document.ArticleId);

                if (alreadyExists == false)
                {
                    var topic = await topicReadService.GetTopicByNameAsync(topicName.ToSnakeCase());

                    var article = articleFactory.Create(Guid.Parse(document.ArticleId), document.PrevTitle, document.Title, document.Content,
                        document.PublishingDate, document.Author, document.Link, topic.TopicId);

                    await articleRepository.AddArticleAsync(article);
                }
            }
        }

        // Method called when the hosted service starts.
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var scope = _serviceProvider.CreateScope();

            var searchServiceDataClient = scope.ServiceProvider.GetRequiredService<ISearchServiceDataClient>();

            var articleReadService = scope.ServiceProvider.GetRequiredService<IArticleReadService>();

            var topicReadService = scope.ServiceProvider.GetRequiredService<ITopicReadService>();

            var articleRepository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();

            var articleFactory = scope.ServiceProvider.GetRequiredService<IArticleFactory>();

            var documentTopic = searchServiceDataClient.GetDocumentsAsync();

            await SeedData(articleReadService, topicReadService, articleRepository, articleFactory, documentTopic);
        }

        // Method called when the hosted service stops.
        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
