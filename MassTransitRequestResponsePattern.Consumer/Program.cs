using MassTransit;
using MassTransit.Transports.Fabric;
using MassTransitRequestResponsePattern.Consumer.Consumers;
using System.Net;

Console.WriteLine("Consumer");

// تنظیمات جدید
string rabbitMqHost = "***.***.***.***";
string userName = "guest";
string password = "guest";

// URI اتصال
var rabbitMqUri = new Uri($"rabbitmq://{rabbitMqHost}");

var bus = Bus.Factory.CreateUsingRabbitMq(configure =>
{
    configure.Host(rabbitMqUri, h =>
    {
        h.Username(userName);
        h.Password(password);
    });

    configure.ReceiveEndpoint("QueuName", ep =>
    {
        ep.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();
