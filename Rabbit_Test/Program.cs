using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RabbitMQ.Client;

namespace Rabbit_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Connection to Rabbit MQ. Write IP if server.
            var factory = new ConnectionFactory() {HostName = "localhost"} ;
            using (var connection = factory.CreateConnection())
            {
                //We need channel to make a connection.(discord ex.)
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "HelloTheFirst",
                        false, //fiziksel olarak ya da bellekte sakla.
                        false, //Bulunduğu queue bağlantısı kapandıktan sonra silinir.
                        false,//Kuyrukta consumer kalmadığında silinir.
                        null);//Collections.Generic.IDictionary(map (dictionary) of arbitrary key/value pairs)

                    string message = "Hello World!";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "hello", null, body);
                    channel.BasicPublish("", "hello", null, Encoding.UTF8.GetBytes("TestOneTwo"));
                    Console.WriteLine("[x] Sent {0}", message);
                }

                
            }

            var conn = factory.CreateConnection();
            var chan = conn.CreateModel();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
