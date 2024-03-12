
using Drahten_Services_UserService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Drahten_Services_UserService.AsyncDataServices
{
    //This is long running background service.
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IEventProcessor _eventProcessor;
        private IConnection? _connection;
        private IModel? _channel;
        private string? _queueName;

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "search_service", type: ExchangeType.Direct);

            _queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: _queueName,
                               exchange: "search_service",
                               routingKey: "search_service.event");

            Console.WriteLine("--> Listening on the message bus.");

            _connection.ConnectionShutdown += RabbitMqConnectionShutDown;
        }

        private void RabbitMqConnectionShutDown(object? sender, ShutdownEventArgs args)
        {
            Console.WriteLine("--> Connection shutdown.");
        }

        public MessageBusSubscriber(IEventProcessor eventProcessor)
        {
            _eventProcessor = eventProcessor;

            InitializeRabbitMq();
        }

        //Listening for events from the message bus (RabbitMq).
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //TODO: When sending request to service (SearchService in this case)
            //      include the user (the sender) id.
            //      The purpose of that is to know to with user to associate the received event
            //      and save the information only for that user.
            //OR 
            //Do this in the EventProcessor. 


            stoppingToken.ThrowIfCancellationRequested();

            if(_channel == null)
            {
                //TODO: Throw custom exception.
            }

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) => {

                Console.WriteLine("--> Event received.");

                var body = ea.Body;

                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if(_channel?.IsOpen ?? false)
            {
                _channel.Close();
                _connection?.Close();
            }

            base.Dispose();
        }
    }
}
