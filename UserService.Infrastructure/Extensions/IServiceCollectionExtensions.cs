using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using UserService.Application.Extensions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Automapper.Profiles;
using UserService.Infrastructure.EntityFramework.Contexts;
using UserService.Infrastructure.EntityFramework.Initialization;
using UserService.Infrastructure.EntityFramework.Options;
using UserService.Infrastructure.EntityFramework.Repositories;
using UserService.Infrastructure.EntityFramework.Services.ReadServices;
using UserService.Infrastructure.Exceptions;
using UserService.Infrastructure.Exceptions.Interfaces;

[assembly: InternalsVisibleTo(assemblyName: "UserService.Tests.EndToEnd")]
namespace UserService.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");

            services.AddDbContext<ReadDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddDbContext<WriteDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddHostedService<DbInitializer>();

            services.AddScoped<IUserRepository, PostgresUserRepository>();

            services.AddScoped<IUserReadService, PostgresUserReadService>();

            services.AddQueriesWithDispatcher();

            services.AddAutoMapper(configAction =>
            {
                configAction.AddProfile<UserProfle>();
                configAction.AddProfile<BannedUserProfile>();
                configAction.AddProfile<ContactRequestProfile>();
            });

            services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

            return services;
        }
    }
}
