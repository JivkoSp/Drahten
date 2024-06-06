
namespace PrivateHistoryService.Infrastructure.EventProcessing
{
    public interface IEventProcessor
    {
        Task ProcessEventAsync(string message);
    }
}
