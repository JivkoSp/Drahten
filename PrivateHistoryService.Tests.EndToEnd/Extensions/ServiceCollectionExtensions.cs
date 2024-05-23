using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PrivateHistoryService.Tests.EndToEnd.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection RemoveDbContext<TContext>(this IServiceCollection services)
           where TContext : DbContext
        {
            var serviceDescriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<TContext>));

            if (serviceDescriptor != null)
            {
                //Remove the TContext from the configured services in Program.cs
                services.Remove(serviceDescriptor);
            }

            return services;
        }
    }
}
