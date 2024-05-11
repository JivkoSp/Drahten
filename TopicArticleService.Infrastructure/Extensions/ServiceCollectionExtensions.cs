using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Application.Services.WriteServices;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Infrastructure.AsyncDataServices;
using TopicArticleService.Infrastructure.Automapper.Profiles;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;
using TopicArticleService.Infrastructure.EntityFramework.Initialization;
using TopicArticleService.Infrastructure.EntityFramework.Options;
using TopicArticleService.Infrastructure.EntityFramework.PrepareDatabase;
using TopicArticleService.Infrastructure.EntityFramework.Repositories;
using TopicArticleService.Infrastructure.EntityFramework.Services;
using TopicArticleService.Infrastructure.EventProcessing;
using TopicArticleService.Infrastructure.Exceptions;
using TopicArticleService.Infrastructure.Exceptions.Interfaces;
using TopicArticleService.Infrastructure.Logging;
using TopicArticleService.Infrastructure.SyncDataServices.Grpc;
using TopicArticleService.Infrastructure.UserRegistration;

[assembly: InternalsVisibleTo(assemblyName: "TopicArticleService.Tests.EndToEnd")]
namespace TopicArticleService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");

            services.AddDbContext<ReadDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddDbContext<WriteDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddScoped<ISearchServiceDataClient, SearchServiceDataClient>();

            services.AddHostedService<DbInitializer>();

            services.AddHostedService<DbPrepper>();

            services.AddScoped<IArticleRepository, PostgresArticleRepository>();

            services.AddScoped<IArticleCommentRepository, PostgresArticleCommentRepository>();

            services.AddScoped<ITopicRepository, PostgresTopicRepository>();

            services.AddScoped<IUserRepository, PostgresUserRepository>();

            services.AddScoped<IArticleReadService, PostgresArticleReadService>();

            services.AddScoped<IUserReadService, PostgresUserReadService>();

            services.AddScoped<IUserWriteService, PostgresUserWriteService>();

            services.AddScoped<ITopicReadService, PostgreTopicReadServices>();

            services.AddQueriesWithDispatcher();

            services.AddAutoMapper(configAction => {

                configAction.AddProfile<ArticleProfile>();
                configAction.AddProfile<ArticleLikeProfile>();
                configAction.AddProfile<ArticleDislikeProfile>();
                configAction.AddProfile<UserProfile>();
                configAction.AddProfile<UserArticleProfile>();
                configAction.AddProfile<ArticleCommentProfile>();
                configAction.AddProfile<ArticleCommentLikeProfile>();
                configAction.AddProfile<ArticleCommentDislikeProfile>();
                configAction.AddProfile<TopicProfile>();
                configAction.AddProfile<DocumentProfile>();
            });

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            services.AddScoped<IUserSynchronizer, UserSynchronizer>();

            services.AddHostedService<MessageBusSubscriber>();

            services.AddSingleton<IEventProcessor, EventProcessor>();

            services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

            return services;
        }
    }
}
