using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UserService.Application.AsyncDataServices;
using UserService.Application.Dtos;

namespace UserService.Infrastructure.AsyncDataServices
{
    internal sealed class MessageBusPublisher : IMessageBusPublisher
    {
        private readonly IConfiguration _configuration;
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

            Console.WriteLine("--> UserService connected to message bus!");

            _connection.ConnectionShutdown += RabbitMqConnectionShutDown;
        }

        private void RabbitMqConnectionShutDown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("--> Connection shutdown.");
        }


        //TODO:
        //Maybe some class that will encapsulate this data (the message, exchange etc.). This way the method will be generic.
        private void SendMessage(string message)
        {
            var messageBody = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "user_service", 
                                  routingKey: "user_service.event", 
                                  body: messageBody);

            Console.WriteLine($"--> UserService have sent a message: {message}!");
        }

        public MessageBusPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeRabbitMq();
        }

        public void PublishNewUser(UserPublishedDto userPublishedDto)
        {
            var message = JsonSerializer.Serialize(userPublishedDto);

            if(_connection.IsOpen)
            {
                Console.WriteLine("--> UserService RabbitMQ connection is OPEN!");

                SendMessage(message);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

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
