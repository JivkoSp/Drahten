using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrivateHistoryService.Infrastructure.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PrivateHistoryService.Infrastructure.AsyncDataServices
{
    //This is long running background service.
    public sealed class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private readonly ILogger<MessageBusSubscriber> _logger;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor, ILogger<MessageBusSubscriber> logger)
        {
            _configuration = configuration;
            _eventProcessor = eventProcessor;
            _logger = logger;

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

            _channel.ExchangeDeclare(exchange: "search_service", type: ExchangeType.Direct);

            _queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: _queueName,
                       exchange: "topic_article_service",
                       routingKey: "topic_article_service.viewed-article");

            _channel.QueueBind(queue: _queueName,
                       exchange: "topic_article_service",
                       routingKey: "topic_article_service.liked-article");

            _channel.QueueBind(queue: _queueName,
                       exchange: "topic_article_service",
                       routingKey: "topic_article_service.disliked-article");

            _channel.QueueBind(queue: _queueName,
                      exchange: "topic_article_service",
                      routingKey: "topic_article_service.commented-article");

            _channel.QueueBind(queue: _queueName,
                     exchange: "topic_article_service",
                     routingKey: "topic_article_service.liked-article-comment");

            _channel.QueueBind(queue: _queueName,
                     exchange: "topic_article_service",
                     routingKey: "topic_article_service.disliked-article-comment");

            _channel.QueueBind(queue: _queueName,
                     exchange: "topic_article_service",
                     routingKey: "topic_article_service.topic-subscription");

            _channel.QueueBind(queue: _queueName,
                     exchange: "search_service",
                     routingKey: "search_service.searched-article-data");

            _logger.LogInformation("PrivateHistoryService --> Listening on the message bus!");

            _connection.ConnectionShutdown += RabbitMqConnectionShutDown;
        }

        private void RabbitMqConnectionShutDown(object sender, ShutdownEventArgs args)
        {
            _logger.LogWarning("PrivateHistoryService --> RabbitMQ connection shutdown!");
        }

        //Listening for events from the message bus (RabbitMQ).
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (ModuleHandle, deliverEventArgs) => {

                _logger.LogInformation("PrivateHistoryService --> RabbitMQ event received!");

                var body = deliverEventArgs.Body;

                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                await _eventProcessor.ProcessEventAsync(notificationMessage);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }

            base.Dispose();
        }
    }
}
