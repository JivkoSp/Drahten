
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullTopicIdException : DomainException
    {
        internal NullTopicIdException() : base(message: "Topic id cannot be null!")
        {
        }
    }
}
