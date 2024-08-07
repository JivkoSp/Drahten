﻿using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Infrastructure.AsyncDataServices;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;
using TopicArticleService.Infrastructure.EntityFramework.PrepareDatabase;
using TopicArticleService.Tests.EndToEnd.Extensions;
using TopicArticleService.Tests.EndToEnd.Services;
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

        private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
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

                services.RemoveHostedService<MessageBusSubscriber>();

                services.RemoveHostedService<DbPrepper>();

                services.AddSingleton<IMessageBusPublisher>(sp =>
                {
                    var uri = _rabbitMqContainer.GetConnectionString();
                    return new RabbitMqMessageBusPublisher(uri);
                });

                services.AddSingleton(sp =>
                {
                    var uri = _rabbitMqContainer.GetConnectionString();
                    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                    return new RabbitMqMessageBusSubscriber(uri, scopeFactory);
                });

                services.AddHostedService(sp => sp.GetRequiredService<RabbitMqMessageBusSubscriber>());
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            await _rabbitMqContainer.StartAsync(); 
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return Task.WhenAll(
                _dbContainer.StopAsync(),
                _rabbitMqContainer.StopAsync()
            );
        }
    }
}
