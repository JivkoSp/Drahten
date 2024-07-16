using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Infrastructure.Exceptions;

namespace UserService.Infrastructure.EntityFramework.Initialization
{
    // This class is responsible for initializing the database by applying any pending migrations.
    internal sealed class DbInitializer : IHostedService
    {
        // An instance of IServiceProvider to access required services.
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //Starts the database initialization process, which applies any pending migrations.
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Retrieve all DbContext types from the current application domain.
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

            // Create a new scope to resolve services.
            var scope = _serviceProvider.CreateScope();

            foreach (var dbContextType in dbContextTypes)
            {
                // Resolve the DbContext instance from the service provider.
                var dbContext = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;

                if (dbContext == null)
                {
                    throw new NullDbContextException();
                }

                Console.WriteLine($"\n\n*** Applying migrations for {dbContext} ... ***\n\n");

                // Apply any pending migrations for the current DbContext.
                await dbContext.Database.MigrateAsync(cancellationToken);

                Console.WriteLine($"\n\n*** Done! All migrations for {dbContext} are applied. ***\n\n");
            }
        }

        // Stops the database initialization process.
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A completed Task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
