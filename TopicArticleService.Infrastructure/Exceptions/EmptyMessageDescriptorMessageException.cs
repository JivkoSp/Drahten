
namespace TopicArticleService.Infrastructure.Exceptions
{
    internal class EmptyMessageDescriptorMessageException : InfrastructureException
    {
        internal EmptyMessageDescriptorMessageException() : base(message: "The Message property of MessageDescriptor cannot be empty!")
        {
        }
    }
}
