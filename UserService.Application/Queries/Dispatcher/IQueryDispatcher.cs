
namespace UserService.Application.Queries.Dispatcher
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query);
    }
}
