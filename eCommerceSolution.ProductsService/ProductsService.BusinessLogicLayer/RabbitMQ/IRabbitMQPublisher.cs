namespace ProductsService.BusinessLogicLayer.RabbitMQ;

public interface IRabbitMQPublisher
{
    Task Publish<T>(string routeKey, T Message);
}
