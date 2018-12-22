using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Rmq.Sample.HelloWorld.Producer
{
    class Program
    {
        const string VHOST = "vhost_demo";
        const string QUEUE = "hello";

        static void Main(string[] args)
        {
            string msg = "Hello, world!";

            if (args != null && args.Length > 0)
            {
                msg = args[0];
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
            using (var conn = connFactory.CreateConnection())
            using (var channel = conn.CreateModel())
            {

                //Decalre queue (it will only be created if it doesn't exist already)
                channel.QueueDeclare(queue: QUEUE, durable: false,
                 exclusive: false, autoDelete: false, arguments: null);


                for (int i = 0; i < 100; i++) //Publish a message every 2 seconds
                { 
                    string finalMsg = $"{msg}({i})";
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(finalMsg)
                        );

                    Console.WriteLine($"{DateTime.Now.ToString()} Mesage sent : {msg}");

                    Thread.Sleep(2000);
                }

            }

            Console.ReadKey();
        }
    }
}
