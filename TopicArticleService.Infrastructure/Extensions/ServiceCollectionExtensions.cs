using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Application.Services.WriteServices;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Infrastructure.Automapper.Profiles;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;
using TopicArticleService.Infrastructure.EntityFramework.Initialization;
using TopicArticleService.Infrastructure.EntityFramework.Options;
using TopicArticleService.Infrastructure.EntityFramework.Repositories;
using TopicArticleService.Infrastructure.EntityFramework.Services;

namespace TopicArticleService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");

            services.AddDbContext<ReadDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddDbContext<WriteDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddHostedService<DbInitializer>();

            services.AddScoped<IArticleRepository, PostgresArticleRepository>();

            services.AddScoped<IArticleCommentRepository, PostgresArticleCommentRepository>();

            services.AddScoped<ITopicRepository, PostgresTopicRepository>();

            services.AddScoped<IArticleReadService, PostgresArticleReadService>();

            services.AddScoped<IUserReadService, PostgresUserReadService>();

            services.AddScoped<IUserWriteService, PostgresUserWriteService>();

            services.AddQueriesWithDispatcher();

            services.AddAutoMapper(configAction => {

                configAction.AddProfile<ArticleProfile>();
                configAction.AddProfile<UserProfile>();
                configAction.AddProfile<UserArticleProfile>();
                configAction.AddProfile<ArticleCommentProfile>();
                configAction.AddProfile<ArticleCommentLikeProfile>();
                configAction.AddProfile<ArticleCommentDislikeProfile>();
            });

            return services;
        }
    }
}
