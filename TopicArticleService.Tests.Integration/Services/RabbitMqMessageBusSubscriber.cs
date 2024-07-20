using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopicArticleService.Tests.Integration.EventProcessing;

namespace TopicArticleService.Tests.Integration.Services
{
    public class RabbitMqMessageBusSubscriber : IDisposable, IHostedService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly string _uri;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEventProcessor _eventProcessor;
        private readonly string _queueName = "test_queue";

        public RabbitMqMessageBusSubscriber(string uri, IServiceScopeFactory scopeFactory, IEventProcessor eventProcessor)
        {
            _uri = uri;
            _scopeFactory = scopeFactory;
            _eventProcessor = eventProcessor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_uri) };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "test_exchange", type: ExchangeType.Direct);

            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            _channel.QueueBind(queue: _queueName, exchange: "test_exchange", routingKey: "topic_article_service.viewed-article");
            _channel.QueueBind(queue: _queueName, exchange: "test_exchange", routingKey: "topic_article_service.liked-article");
            _channel.QueueBind(queue: _queueName, exchange: "test_exchange", routingKey: "topic_article_service.disliked-article");
            _channel.QueueBind(queue: _queueName, exchange: "test_exchange", routingKey: "topic_article_service.commented-article");
            _channel.QueueBind(queue: _queueName, exchange: "test_exchange", routingKey: "topic_article_service.liked-article-comment");
            _channel.QueueBind(queue: _queueName, exchange: "test_exchange", routingKey: "topic_article_service.disliked-article-comment");
            _channel.QueueBind(queue: _queueName, exchange: "test_exchange", routingKey: "topic_article_service.topic-subscription");

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                _eventProcessor.ProcessEvent(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_channel?.IsOpen ?? false)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
