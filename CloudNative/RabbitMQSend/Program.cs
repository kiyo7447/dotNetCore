using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQSend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				ConsoleKeyInfo ck = new ConsoleKeyInfo();
				while (ck.Key != ConsoleKey.E)
				{
					channel.QueueDeclare(queue: "hello",
									 durable: false,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);
					string message = $"Hello World!{ck.Key}";
					var body = Encoding.UTF8.GetBytes(message);
					channel.BasicPublish(exchange: "",
									 routingKey: "hello",
									 basicProperties: null,
									 body: body);
					Console.WriteLine(" [x] Sent {0}", message);
					ck = Console.ReadKey();
				}
			}

			Console.WriteLine(" Press [enter] to exit.");
		}
    }
}
