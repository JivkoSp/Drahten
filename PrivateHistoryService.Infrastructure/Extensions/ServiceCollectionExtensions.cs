using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;
using PrivateHistoryService.Infrastructure.EntityFramework.Initialization;
using PrivateHistoryService.Infrastructure.EntityFramework.Options;
using PrivateHistoryService.Infrastructure.EntityFramework.Repositories;
using PrivateHistoryService.Infrastructure.Automapper.Profiles;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Services.ReadServices;
using PrivateHistoryService.Infrastructure.Exceptions.Interfaces;
using PrivateHistoryService.Infrastructure.Exceptions;
using PrivateHistoryService.Infrastructure.UserRegistration;
using PrivateHistoryService.Infrastructure.AsyncDataServices;
using PrivateHistoryService.Infrastructure.EventProcessing;
using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");

            services.AddSingleton<IEncryptionProvider>(new EncryptionProvider("A1B2C3D4E5F60789"));

            services.AddDbContext<ReadDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddDbContext<WriteDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddHostedService<DbInitializer>();

            services.AddScoped<IUserRepository, PostgresUserRepository>();

            services.AddScoped<IUserReadService, PostgresUserReadService>();

            services.AddScoped<ICommentedArticleReadService, PostgresCommentedArticleReadService>();

            services.AddScoped<ISearchedArticleDataReadService, PostgresSearchedArticleDataReadService>();

            services.AddScoped<IViewedArticleReadService, PostgresViewedArticleReadService>();

            services.AddScoped<ISearchedTopicDataReadService, PostgresSearchedTopicDataReadService>();

            services.AddScoped<IViewedUserReadService, PostgresViewedUserReadService>();

            services.AddScoped<IViewedArticleWriteService, PostgresViewedArticleWriteService>();

            services.AddScoped<ILikedArticleWriteService, PostgresLikedArticleWriteService>();

            services.AddScoped<IDislikedArticleWriteService, PostgresDislikedArticleWriteService>();

            services.AddScoped<ICommentedArticleWriteService, PostgresCommentedArticleWriteService>();

            services.AddScoped<ILikedArticleCommentWriteService, PostgresLikedArticleCommentWriteService>();

            services.AddScoped<IDislikedArticleCommentWriteService, PostgresDislikedArticleCommentWriteService>();

            services.AddScoped<ITopicSubscriptionWriteService, PostgresTopicSubscriptionWriteService>();

            services.AddQueriesWithDispatcher();

            services.AddAutoMapper(configAction =>
            {
                configAction.AddProfile<CommentedArticleProfile>();
                configAction.AddProfile<DislikedArticleCommentProfile>();
                configAction.AddProfile<DislikedArticleProfile>();
                configAction.AddProfile<LikedArticleCommentProfile>();
                configAction.AddProfile<LikedArticleProfile>();
                configAction.AddProfile<SearchedArticleDataProfile>();
                configAction.AddProfile<SearchedTopicDataProfile>();
                configAction.AddProfile<TopicSubscriptionProfile>();
                configAction.AddProfile<ViewedArticleProfile>();
                configAction.AddProfile<ViewedUserProfile>();
            });

            services.AddScoped<IUserSynchronizer, UserSynchronizer>();

            services.AddSingleton<IEventProcessor, EventProcessor>();

            services.AddHostedService<MessageBusSubscriber>();

            services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

            return services;
        }
    }
}
