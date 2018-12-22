using System;
using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rmq.Sample.HelloWorld.Consumer
{
    class Program
    {
        const string QUEUE_NAME = "hello";
        static void Main(string[] args)
        {
            //Declare RabbitMQ connenction factory
            var connFactory = new ConnectionFactory()
            {
                HostName = "jb.com",
                Port = 5672
            };

            var conn = connFactory.CreateConnection();
            var channel = conn.CreateModel();

            //Decalre queue (it will only be created if it doesn't exist already)
            channel.QueueDeclare(
                queue: QUEUE_NAME,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            //Listen and receiving the messages
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var msg = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine($"{DateTime.Now.ToString()} Mesage received : {msg}");
            };

            channel.BasicConsume(queue: QUEUE_NAME, autoAck: true, consumer: consumer);

            Console.ReadKey();
            channel.Dispose();
            conn.Dispose();
        }
    }
}
