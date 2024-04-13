
namespace UserService.Application.Commands.Handlers
{
    internal interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
