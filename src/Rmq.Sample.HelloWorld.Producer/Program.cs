using System;
using System.Text;
using RabbitMQ.Client;

namespace Rmq.Sample.HelloWorld.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var connFactory = new ConnectionFactory()
            {
                HostName = "jb.com",
                Port = 5672
            };

            using (var conn = connFactory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false,
                 exclusive: false, autoDelete: false, arguments: null);

                string msg = "Hello World!";

                channel.BasicPublish(
                    exchange: "", 
                    routingKey: "hello",
                    basicProperties: null, 
                    body: Encoding.UTF8.GetBytes(msg));

                 Console.WriteLine($"Message sent : {msg}");  
            }

            Console.ReadKey();
        }
    }
}
