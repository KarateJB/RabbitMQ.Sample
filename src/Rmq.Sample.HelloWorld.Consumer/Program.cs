using System;
using RabbitMQ.Client;

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

            using(var conn = connFactory.CreateConnection())
            using(var channel = conn.CreateModel())
            {
                //Decalre queue (it will only be created if it doesn't exist already)
                channel.QueueDeclare(
                    queue: QUEUE_NAME,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
            }
        }
    }
}
