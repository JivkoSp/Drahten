using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PublicHistoryService.Infrastructure.Exceptions;

namespace PublicHistoryService.Infrastructure.EntityFramework.Initialization
{
    internal sealed class DbInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

            var scope = _serviceProvider.CreateScope();

            foreach (var dbContextType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;

                if (dbContext == null)
                {
                    throw new NullDbContextException();
                }

                Console.WriteLine($"\n\n*** Applying migrations for {dbContext} ... ***\n\n");

                await dbContext.Database.MigrateAsync(cancellationToken);

                Console.WriteLine($"\n\n*** Done! All migrations for {dbContext} are applied. ***\n\n");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
