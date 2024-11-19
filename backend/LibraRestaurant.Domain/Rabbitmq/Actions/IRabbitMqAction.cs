using RabbitMQ.Client;

namespace LibraRestaurant.Domain.Rabbitmq.Actions;

public interface IRabbitMqAction
{
    void Perform(IModel channel);
}