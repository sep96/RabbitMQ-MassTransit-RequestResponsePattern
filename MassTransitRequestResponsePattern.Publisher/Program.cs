using MassTransit;
using Shared.RequestResponseMessages;

Console.WriteLine("Publisher");
 
string rabbitMqHost = "***.***.***.***";
string userName = "guest";
string password = "guest";

var rabbitMqUri = new Uri($"rabbitmq://{rabbitMqHost}");

var bus = Bus.Factory.CreateUsingRabbitMq(configure =>
{
    configure.Host(rabbitMqUri, h =>
    {
        h.Username(userName);
        h.Password(password);
    });
});

string queueName = "RabbiyttQueue";

await bus.StartAsync();
IRequestClient<RequestMessage> request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMqUri}/{queueName}"));

int i = 0;
while (true)
{
    i++;
    await Task.Delay(1000);
    Response<ResponseMessage> response = await request.GetResponse<ResponseMessage>(new RequestMessage { MessageNo = i, Text = $"{i}. request" });
    Console.WriteLine($"Response Received: {response.Message.Text}");
}

Console.Read();
