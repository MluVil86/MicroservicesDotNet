using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProductsService.BusinessLogicLayer.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
    private readonly IConfiguration _configuration;
    private IChannel _channel = default!;
    private IConnection _connection = default!;

    public RabbitMQPublisher(IConfiguration configuration)
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
            AutomaticRecoveryEnabled =true,
            TopologyRecoveryEnabled = true
        };

        _connection = await connectionFactory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();        
    }
    
    public async Task Publish<T>(string routeKey, T Message)
    {
        string MessageJson = JsonSerializer.Serialize(Message);

        byte[] messageBodyInBytes = Encoding.UTF8.GetBytes(MessageJson);

        //create exchange
        string exchangeName = _configuration["RABBITMQ_PRODUCTS_EXCHANGE"]!;
        await _channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct, durable: true);

        //publish message
        await _channel.BasicPublishAsync(exchange: exchangeName, routingKey:routeKey, body: messageBodyInBytes);        
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
