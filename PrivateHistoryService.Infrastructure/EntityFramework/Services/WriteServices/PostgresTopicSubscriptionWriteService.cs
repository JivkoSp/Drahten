using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresTopicSubscriptionWriteService : ITopicSubscriptionWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresTopicSubscriptionWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task AddTopicSubscriptionAsync(TopicSubscription topicSubscription)
        {
            await _writeDbContext.TopicSubscriptions.AddAsync(topicSubscription);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
