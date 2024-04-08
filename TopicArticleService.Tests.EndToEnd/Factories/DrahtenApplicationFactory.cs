using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;
using TopicArticleService.Tests.EndToEnd.Extensions;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Factories
{
    public sealed class DrahtenApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres:14.2")
                .WithDatabase("TopicArticleServiceDb")
                .WithUsername("TopicArticleServiceAdmin")
                .WithPassword("password")
                .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            //Configure dependency injection ONLY for tests that are using this custom factory (e.g DrahtenApplicationFactory).
            builder.ConfigureTestServices(services =>
            {
                services.RemoveDbContext<ReadDbContext>();

                services.RemoveDbContext<WriteDbContext>();

                services.AddDbContext<ReadDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<WriteDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });
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
