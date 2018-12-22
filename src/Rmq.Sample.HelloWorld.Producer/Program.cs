using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Rmq.Sample.HelloWorld.Producer
{
    class Program
    {
        const string QUEUE_NAME = "hello";

        static void Main(string[] args)
        {
            string msg = "Hello, world!";

            if (args!=null && args.Length > 0)
            {
                msg = args[0];
            }

            var connFactory = new ConnectionFactory()
            {
                HostName = "jb.com",
                Port = 5672
            };

            using (var conn = connFactory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: QUEUE_NAME, durable: false,
                 exclusive: false, autoDelete: false, arguments: null);

                 
                for(int i=0; i<100; i++){
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes($"{msg}({i})")
                        );

                    Console.WriteLine($"{DateTime.Now.ToString()} Mesage sent : {msg}");

                    Thread.Sleep(3000);
                } 

            }

            Console.ReadKey();
        }
    }
}
