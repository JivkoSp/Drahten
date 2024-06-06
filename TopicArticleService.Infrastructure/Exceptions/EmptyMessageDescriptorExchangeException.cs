
namespace TopicArticleService.Infrastructure.Exceptions
{
    internal class EmptyMessageDescriptorExchangeException : InfrastructureException
    {
        internal EmptyMessageDescriptorExchangeException() : base(message: "The Exchange property of MessageDescriptor cannot be empty!")
        {
        }
    }
}
