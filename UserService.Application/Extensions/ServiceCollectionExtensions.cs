using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserService.Application.Commands.Dispatcher;
using UserService.Application.Commands.Handlers;
using UserService.Application.Queries.Dispatcher;
using UserService.Application.Queries.Handlers;
using UserService.Domain.Factories;
using UserService.Domain.Factories.Interfaces;

namespace UserService.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddCommandsWithDispatcher(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();

            services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();

            //Adding automatic registration for all command handlers.
            //Reason: There is possibility to forget to register command handler in the DI container.
            //        Every time that new command handler is created it has to be registered in the DI container,
            //        so in addition this eliminates duplicate work.
            services.Scan(x => x.FromAssemblies(assembly).AddClasses(x =>
                x.AssignableTo(typeof(ICommandHandler<>))).AsImplementedInterfaces().WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddQueriesWithDispatcher(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();

            services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();

            //Adding automatic registration for all query handlers.
            //Reason: There is possibility to forget to register query handler in the DI container.
            //        Every time that new query handler is created it has to be registered in the DI container,
            //        so in addition this eliminates duplicate work.
            services.Scan(x => x.FromAssemblies(assembly).AddClasses(x =>
                x.AssignableTo(typeof(IQueryHandler<,>))).AsImplementedInterfaces().WithScopedLifetime());

            return services;
        }

        internal static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddSingleton<IUserFactory, UserFactory>();
           
            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommandsWithDispatcher();

            services.AddFactories();

            return services;
        }
    }
}
