namespace OrderService.BusinessLogicLayer.RabbitMQ
{
    public interface IRabbitMQProductNameConsumer
    {
        Task Consume<T>();
    }
}
