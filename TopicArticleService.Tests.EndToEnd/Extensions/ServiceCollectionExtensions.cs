using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TopicArticleService.Tests.EndToEnd.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void Remove<TService>(this IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(TService));

            if(descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        public static void RemoveHostedService<TService>(this IServiceCollection services) where TService : class, IHostedService
        {
            var descriptor = services.SingleOrDefault(x =>
                x.ServiceType == typeof(IHostedService) && x.ImplementationType == typeof(TService));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }
    }
}
