using Drahten_Services_UserService.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Text.Json;

namespace Drahten_Services_UserService.EventProcessing
{
    enum EventType
    {
        SearchInformationPublished,
        Undetermined
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event Type.");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType?.Event)
            {
                case "Search_Information_Published":
                    Console.WriteLine("--> Search event detected.");
                    return EventType.SearchInformationPublished;
                default:
                    Console.WriteLine("--> Could NOT determine the event type.");
                    return EventType.Undetermined;
            }
        }

        public EventProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.SearchInformationPublished:
                    break;
            }
        }
    }
}
