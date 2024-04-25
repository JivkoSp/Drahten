using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TopicArticleService.Infrastructure.EventProcessing;

namespace TopicArticleService.Infrastructure.AsyncDataServices
{
    //This is long running background service.
    public sealed class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "user_service", type: ExchangeType.Direct);

            _queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: _queueName,
                               exchange: "user_service",
                               routingKey: "user_service.event");

            Console.WriteLine("--> TopicArticleService listening on the message bus!");

            _connection.ConnectionShutdown += RabbitMqConnectionShutDown;
        }

        private void RabbitMqConnectionShutDown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("--> TopicArticleService connection shutdown!");
        }

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            _configuration = configuration;
            _eventProcessor = eventProcessor;
            InitializeRabbitMq();
        }

        //Listening for events from the message bus (RabbitMQ).
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (ModuleHandle, deliverEventArgs) => {

                Console.WriteLine("--> TopicArticleService event received!");

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
