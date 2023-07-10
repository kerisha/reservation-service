using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
string toNumber = Environment.GetEnvironmentVariable("TWILIO_TO_NUMBER");
string fromNumber = Environment.GetEnvironmentVariable("TWILIO_FROM_NUMBER");


ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
factory.UserName = "guest";
factory.Password = "guest";
IConnection conn = factory.CreateConnection();
IModel channel = conn.CreateModel();
channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    SendSMS(message);
    Console.WriteLine(" [x] Received from Rabbit: {0}", message);
};
Console.WriteLine("Consuming Queue Now");

channel.BasicConsume(queue: "hello",
                        autoAck: true,
                        consumer: consumer);


void SendSMS(string text)
{
    Console.WriteLine("Sending SMS");
    try{
        TwilioClient.Init(accountSid, authToken);

        var message = MessageResource.Create(
            body: text,
            from: new Twilio.Types.PhoneNumber(fromNumber),
            to: new Twilio.Types.PhoneNumber(toNumber)
        );
        Console.WriteLine(message.Sid);
        Console.WriteLine("SMS Sent");
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Exception occurred: {ex}");
    }


}


while(true)
{
    Thread.Sleep(1 * 1000);
}
