using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublicHistoryService.Application.Commands.Handlers;
using PublicHistoryService.Application.Extensions;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Infrastructure.Automapper.Profiles;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PublicHistoryService.Infrastructure.EntityFramework.Initialization;
using PublicHistoryService.Infrastructure.EntityFramework.Options;
using PublicHistoryService.Infrastructure.EntityFramework.Repositories;
using PublicHistoryService.Infrastructure.EntityFramework.Services.ReadServices;
using PublicHistoryService.Infrastructure.Exceptions;
using PublicHistoryService.Infrastructure.Exceptions.Interfaces;
using PublicHistoryService.Infrastructure.Logging;
using PublicHistoryService.Infrastructure.UserRegistration;

namespace PublicHistoryService.Infrastructure.Extensions
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

            services.AddQueriesWithDispatcher();

            services.AddAutoMapper(configAction =>
            {
                configAction.AddProfile<CommentedArticleProfile>();
                configAction.AddProfile<SearchedArticleDataProfile>();
                configAction.AddProfile<SearchedTopicDataProfile>();
                configAction.AddProfile<ViewedArticleProfile>();
                configAction.AddProfile<ViewedUserProfile>();
            });

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            services.AddScoped<IUserSynchronizer, UserSynchronizer>();

            services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

            return services;
        }
    }
}
