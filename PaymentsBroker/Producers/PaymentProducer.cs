using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace PaymentsBroker.Producers;

public class PaymentProducer
{
    public void PaymentConfirmed<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "host.docker.internal"
        };

        var connection = factory.CreateConnection();
        
        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "payment_broker_exchange", ExchangeType.Direct, durable: true);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.QueueDeclare("payment_confirmed", exclusive: false, durable: true, autoDelete: false);

        channel.QueueBind(exchange: "payment_broker_exchange", queue: "payment_confirmed", routingKey: "payment");

        channel.BasicPublish(exchange: "payment_broker_exchange", routingKey: "payment", body: body);
    }
}