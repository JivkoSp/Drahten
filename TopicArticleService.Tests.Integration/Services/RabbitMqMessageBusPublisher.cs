using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;

namespace TopicArticleService.Tests.Integration.Services
{
    public class RabbitMqMessageBusPublisher : IMessageBusPublisher
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly string _uri;

        public RabbitMqMessageBusPublisher(string uri)
        {
            _uri = uri;

            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(_uri)
                };

                _connection = factory.CreateConnection();

                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "test_exchange", type: ExchangeType.Direct);

                _connection.ConnectionShutdown += RabbitMqConnectionShutDown;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize RabbitMQ connection: {ex.Message}");
            }
        }

        private void RabbitMqConnectionShutDown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("TopicArticleService --> RabbitMQ connection shutdown.");
        }

        private void SendMessage(MessageDescriptor messageDescriptor)
        {
            try
            {
                var messageBody = Encoding.UTF8.GetBytes(messageDescriptor.Message);

                _channel.BasicPublish(exchange: messageDescriptor.Exchange,
                                        routingKey: messageDescriptor.RoutingKey,
                                        body: messageBody);

                Console.WriteLine($"Message was send to RabbitMQ.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message sending {ex}.");
            }
        }

        public Task PublishViewedArticleAsync(ViewedArticleDto viewedArticleDto)
        {
            try
            {
                var message = JsonSerializer.Serialize(viewedArticleDto);

                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "test_exchange", routingKey: "topic_article_service.viewed-article");

                SendMessage(messageDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message publishing {ex}.");
            }

            return Task.CompletedTask;
        }

        public Task PublishLikedArticleAsync(LikedArticleDto likedArticleDto)
        {
            try
            {
                var message = JsonSerializer.Serialize(likedArticleDto);

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "test_exchange", routingKey: "topic_article_service.liked-article");

                SendMessage(messageDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message publishing {ex}.");
            }

            return Task.CompletedTask;
        }

        public Task PublishDislikedArticleAsync(DislikedArticleDto dislikedArticleDto)
        {
            try
            {
                var message = JsonSerializer.Serialize(dislikedArticleDto);

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "test_exchange", routingKey: "topic_article_service.disliked-article");

                SendMessage(messageDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message sending");
            }

            return Task.CompletedTask;
        }

        public Task PublishCommentedArticleAsync(CommentedArticleDto commentedArticleDto)
        {
            var message = JsonSerializer.Serialize(commentedArticleDto);

            try
            {
                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "test_exchange", routingKey: "topic_article_service.commented-article");

                SendMessage(messageDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message publishing {ex}.");
            }

            return Task.CompletedTask;
        }

        public Task PublishLikedArticleCommentAsync(LikedArticleCommentDto likedArticleCommentDto)
        {
            try
            {
                var message = JsonSerializer.Serialize(likedArticleCommentDto);

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "test_exchange", routingKey: "topic_article_service.liked-article-comment");

                SendMessage(messageDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message publishing {ex}.");
            }

            return Task.CompletedTask;
        }

        public Task PublishDislikedArticleCommentAsync(DislikedArticleCommentDto dislikedArticleCommentDto)
        {
            try
            {
                var message = JsonSerializer.Serialize(dislikedArticleCommentDto);

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "test_exchange", routingKey: "topic_article_service.disliked-article-comment");

                SendMessage(messageDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message publishing {ex}.");
            }

            return Task.CompletedTask;
        }

        public Task PublishTopicSubscriptionAsync(TopicSubscriptionDto topicSubscriptionDto)
        {
            try
            {
                var message = JsonSerializer.Serialize(topicSubscriptionDto);

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "test_exchange", routingKey: "topic_article_service.topic-subscription");

                SendMessage(messageDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message publishing {ex}.");
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
