
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptyTopicIdException : DomainException
    {
        public EmptyTopicIdException() : base(message: "Topic id cannot be empty!")
        {
        }
    }
}
