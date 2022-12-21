using Client2.Enum.v1;
using Client2.Models.v1;
using Client2.Repositories.v1;
using Client2.Services.v1;
using StackExchange.Redis;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client2
{
    public class Program
    {
        private const string RedisConnectionString = "localhost:6379";
        private static ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel2 = "Channel2";
        private const string Channel = "Channel1";
        static ISubscriber pubsub = connection.GetSubscriber();


        static void Main(string[] args)
        {
            Console.WriteLine("Status Order");
            Product product;
            pubsub.SubscribeAsync(Channel2, (channel, message) =>
            {
                product = JsonSerializer.Deserialize<Product>(message);
                pubsub.PublishAsync(Channel, "Data received for validation.", CommandFlags.FireAndForget);
                ValidatorServices.CheckData(product);
            });

            pubsub.Subscribe(Channel, (channel, message) =>
            {
                Console.Write($"\n{message}");
            });
            Console.ReadLine();
        }
    }
}
