using System.Text.Json;
using PaymentsBroker.Messages;
using PaymentsBroker.Mongo;
using PaymentsBroker.Repository;

namespace PaymentsBroker.Consumers;
using System;
using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class PaymentConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly PaymentRepository _paymentRepository;

    public PaymentConsumer(IServiceProvider serviceProvider, PaymentRepository paymentRepository)
    {
        _serviceProvider = serviceProvider;
        _paymentRepository = paymentRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "host.docker.internal"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "payments",
            durable: false,
            exclusive: false,
            autoDelete: true,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            PaymentMessage? paymentMessage = JsonSerializer.Deserialize<PaymentMessage>(message);

            Console.WriteLine($"Received message: {message}");

            PaymentEventDocument pd = new PaymentEventDocument()
            {
                OrderId = paymentMessage.OrderId,
                Price = paymentMessage.Price,
                PSP = paymentMessage.PSP,
                Timestamp = paymentMessage.Timestamp
            };

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                PaymentRepository _paymentRepository =
                    scope.ServiceProvider.GetRequiredService<PaymentRepository>();
                
                await _paymentRepository.AddPayment(pd);
                
            }

            await Task.Yield();
        };

        channel.BasicConsume(queue: "payments",
            autoAck: true,
            consumer: consumer);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken); // Wait for new messages
        }
        
    }
}