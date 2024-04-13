using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Commands.Handlers;

namespace UserService.Application.Commands.Dispatcher
{
    internal sealed class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            using var scope = _serviceProvider.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

            await handler.HandleAsync(command);
        }
    }
}
