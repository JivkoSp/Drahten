using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using UserService.Application.AsyncDataServices;
using UserService.Infrastructure.EntityFramework.Contexts;
using UserService.Tests.EndToEnd.Extensions;
using Xunit;

namespace UserService.Tests.EndToEnd.Factories
{
    public sealed class UserServiceApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres:14.2")
                .WithDatabase("UserServiceDb")
                .WithUsername("UserServiceAdmin")
                .WithPassword("password")
                .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            //Configure dependency injection ONLY for tests that are using this custom factory (e.g DrahtenApplicationFactory).
            builder.ConfigureTestServices(services =>
            {
                services.Remove<DbContextOptions<ReadDbContext>>();

                services.Remove<DbContextOptions<WriteDbContext>>();

                services.AddDbContext<ReadDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<WriteDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });

                services.Remove<IMessageBusPublisher>();
            });
        }

        public Task InitializeAsync()
        {
            return _dbContainer.StartAsync();
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}
