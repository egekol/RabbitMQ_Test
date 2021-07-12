using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Receiving
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                //Neden burada da queue başlattık? Çünkü bağlantıyı producerdan önce consumer da başlatmak isteyebilir. Mesajlar alınmadan önce queue'nun varlığından emin olunmalı.

                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "HelloTheFirst",
                         false,
                         false,
                         false,
                         null);

                    Console.WriteLine(" test");
                    //servera mesajları queue'dan çekeceğimizi söylemeliyiz. Mesajlar geldiği anda çekmeyeceğimiz için bunu bildirmeliyiz:
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };
                    channel.BasicConsume(queue: "hello",
                        autoAck: true,
                        consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();



                }


            }
        }
    }
}
