
namespace PrivateHistoryService.Application.Dtos
{
    public class TopicSubscriptionDto
    {
        public Guid TopicId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
