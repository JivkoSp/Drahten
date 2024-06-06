using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;

namespace TopicArticleService.Infrastructure.AsyncDataServices
{
    internal sealed class MessageBusPublisher : IMessageBusPublisher
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "topic_article_service", type: ExchangeType.Direct);

            _connection.ConnectionShutdown += RabbitMqConnectionShutDown;
        }

        private void RabbitMqConnectionShutDown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("--> Connection shutdown.");
        }

        private void SendMessage(MessageDescriptor messageDescriptor)
        {
            var messageBody = Encoding.UTF8.GetBytes(messageDescriptor.Message);

            _channel.BasicPublish(exchange: messageDescriptor.Exchange,
                                  routingKey: messageDescriptor.RoutingKey,
                                  body: messageBody);

            //TODO: Log the message.
        }
        public void PublishViewedArticle(ViewedArticleDto viewedArticleDto)
        {
            var message = JsonSerializer.Serialize(viewedArticleDto);

            if (_connection.IsOpen)
            {
                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message, 
                    exchange: "topic_article_service", routingKey: "topic_article_service.viewed-article");

                SendMessage(messageDescriptor);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            }
        }

        public void PublishLikedArticle(LikedArticleDto likedArticleDto)
        {
            var message = JsonSerializer.Serialize(likedArticleDto);

            if (_connection.IsOpen)
            {
                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "topic_article_service", routingKey: "topic_article_service.liked-article");

                SendMessage(messageDescriptor);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            }
        }

        public void PublishDislikedArticle(DislikedArticleDto dislikedArticleDto)
        {
            var message = JsonSerializer.Serialize(dislikedArticleDto);

            if (_connection.IsOpen)
            {
                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "topic_article_service", routingKey: "topic_article_service.disliked-article");

                SendMessage(messageDescriptor);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            }
        }

        public void PublishCommentedArticle(CommentedArticleDto commentedArticleDto)
        {
            var message = JsonSerializer.Serialize(commentedArticleDto);

            if (_connection.IsOpen)
            {
                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "topic_article_service", routingKey: "topic_article_service.commented-article");

                SendMessage(messageDescriptor);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            }
        }

        public void PublishLikedArticleComment(LikedArticleCommentDto likedArticleCommentDto)
        {
            var message = JsonSerializer.Serialize(likedArticleCommentDto);

            if (_connection.IsOpen)
            {
                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "topic_article_service", routingKey: "topic_article_service.liked-article-comment");

                SendMessage(messageDescriptor);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            }
        }

        public void PublishDislikedArticleComment(DislikedArticleCommentDto dislikedArticleCommentDto)
        {
            var message = JsonSerializer.Serialize(dislikedArticleCommentDto);

            if (_connection.IsOpen)
            {
                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "topic_article_service", routingKey: "topic_article_service.disliked-article-comment");

                SendMessage(messageDescriptor);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            }
        }

        public void PublishTopicSubscription(TopicSubscriptionDto topicSubscriptionDto)
        {
            var message = JsonSerializer.Serialize(topicSubscriptionDto);

            if (_connection.IsOpen)
            {
                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "topic_article_service", routingKey: "topic_article_service.topic-subscription");

                SendMessage(messageDescriptor);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            }
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
