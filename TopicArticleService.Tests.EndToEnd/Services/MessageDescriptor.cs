
using TopicArticleService.Infrastructure.Exceptions;

namespace TopicArticleService.Tests.EndToEnd.Services
{
    internal sealed class MessageDescriptor
    {
        public string Message { get; }
        public string Exchange { get; }
        public string RoutingKey { get; }

        internal MessageDescriptor(string message, string exchange, string routingKey)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new EmptyMessageDescriptorMessageException();
            }

            if (string.IsNullOrWhiteSpace(exchange))
            {
                throw new EmptyMessageDescriptorExchangeException();
            }

            if (string.IsNullOrWhiteSpace(routingKey))
            {
                throw new EmptyMessageDescriptorRoutingKeyException();
            }

            Message = message;
            Exchange = exchange;
            RoutingKey = routingKey;
        }
    }
}
