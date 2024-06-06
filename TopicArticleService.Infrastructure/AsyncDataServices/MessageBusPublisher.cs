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
                //TODO: Log the message.

                var messageDescriptor = new MessageDescriptor(message, 
                    exchange: "topic_article_service", routingKey: "topic_article_service.viewed-article");

                SendMessage(messageDescriptor);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> UserService RabbitMQ connection is CLOSED!");
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
