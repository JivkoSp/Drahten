
namespace TopicArticleService.Infrastructure.Exceptions
{
    internal class EmptyMessageDescriptorRoutingKeyException : InfrastructureException
    {
        internal EmptyMessageDescriptorRoutingKeyException() : base(message: "The RoutingKey property of MessageDescriptor cannot be empty!")
        {
        }
    }
}
