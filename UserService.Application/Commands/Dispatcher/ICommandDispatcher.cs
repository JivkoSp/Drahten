
namespace UserService.Application.Commands.Dispatcher
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
