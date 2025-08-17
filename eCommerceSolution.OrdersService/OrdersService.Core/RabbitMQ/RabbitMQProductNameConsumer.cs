using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.RabbitMQ
{
    public class RabbitMQProductNameConsumer : IRabbitMQProductNameConsumer, IDisposable
    {
        private readonly IConfiguration _configuration;
        private IChannel _channel = default!;
        private IConnection _connection = default!;

        public RabbitMQProductNameConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            Task.Run(async () => await InitializeAsync()).GetAwaiter().GetResult();
        }

        private async Task InitializeAsync()
        {
            string hostName = _configuration["RABBITMQ_HOSTNAME"]!;
            string portName = _configuration["RABBITMQ_PORT"]!;
            string userName = _configuration["RABBITMQ_USERNAME"]!;
            string password = _configuration["RABBITMQ_PASSWORD"]!;

            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
                Port = Convert.ToInt32(portName),
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true
            };

            _connection = await connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
        }

        public async Task Consume<T>(string routeKey, T Message)
        {
            string routingKey = "product.update.name";
            string queueName = "order.product.update.name.queue";

            //create exchange
            string exchangeName = _configuration["RABBITMQ_PRODUCTS_EXCHANGE"]!;

            //create message queue
            await _channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete:false, arguments: null);

            //bind the message to exchange
            await _channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: routingKey);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
