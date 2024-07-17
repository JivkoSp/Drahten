using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using Microsoft.Extensions.Hosting;
using TopicArticleService.Tests.Integration.Events;

namespace TopicArticleService.Tests.Integration.Services
{
    public class RabbitMqMessageBusSubscriber : IDisposable, IHostedService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly string _uri;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string _queueName = "test_queue";
        public List<ITestEvent> Events = new List<ITestEvent>();

        public RabbitMqMessageBusSubscriber(string uri, IServiceScopeFactory scopeFactory)
        {
            _uri = uri;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_uri) };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "test_exchange", type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _queueName, exchange: "test_exchange", routingKey: "topic_article_service.viewed-article");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await ProcessMessageAsync(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private Task ProcessMessageAsync(string message)
        {
            var viewedArticleDto = JsonSerializer.Deserialize<ViewedArticleDto>(message);

            using (var scope = _scopeFactory.CreateScope())
            {
                Events.Add(new ViewedArticleAdded(viewedArticleDto.ArticleId));

                return Task.CompletedTask;
            }
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
