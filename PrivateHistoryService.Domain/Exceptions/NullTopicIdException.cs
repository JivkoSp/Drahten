
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullTopicIdException : DomainException
    {
        public NullTopicIdException() : base(message: "Topic id cannot be null!")
        {
        }
    }
}
