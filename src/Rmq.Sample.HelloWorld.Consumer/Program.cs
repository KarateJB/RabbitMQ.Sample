using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rmq.Sample.HelloWorld.Consumer
{
    class Program
    {
        const string VHOST = "vhost_demo";
        const string QUEUE = "hello";
        static void Main(string[] args)
        {
            int priority = 0;

            if (args != null && args.Length > 0)
            {
                int.TryParse(args[0], out priority);
            }

            //Declare RabbitMQ connenction factory
            var connFactory = new ConnectionFactory()
            {
                HostName = "jb.com",
                Port = 5672,
                VirtualHost = VHOST,
                UserName="rabbitmquser",
                Password="rabbitmqpwd"
            };

            //Create connection and channel
            var conn = connFactory.CreateConnection();
            var channel = conn.CreateModel();

            //Decalre queue (it will only be created if it doesn't exist already)
            channel.QueueDeclare(
                queue: QUEUE,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            //Listen and receiving the messages
            //Define the callback for receiving message by "EventingBasicConsumer.Received"
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var msg = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine($"{DateTime.Now.ToString()} Mesage received : {msg}");
            };

            //Start consuming
            var consumeArgs = new Dictionary<string, object>();
            consumeArgs.Add("x-priority", priority);


            channel.BasicConsume(
                queue: QUEUE, 
                autoAck: true, 
                consumer: consumer, arguments: consumeArgs);

            Console.ReadKey();
            channel.Close();
            conn.Close();
        }
    }
}
