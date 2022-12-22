using Client2.Models.v1;
using StackExchange.Redis;
using System;
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


        static async Task Main(string[] args)
        {
            Console.WriteLine("Status Order");
            Product product;
            await Task.Run(async () => await pubsub.SubscribeAsync(Channel2, async (channel, message) => 
            {
                product = JsonSerializer.Deserialize<Product>(message);
                await Task.Run(async () => product = await Services.v1.Services.Post(product));
                await Task.Run(async () => await pubsub.PublishAsync(Channel, $"Data received for validation. Number order: {product.Id}", CommandFlags.FireAndForget));
                await Task.Run(async () => await Services.v1.Services.CheckData(product));
            }));

            await Task.Run(async () => await pubsub.SubscribeAsync(Channel, (channel, message) =>
            {
                Console.Write($"\n{message}");
            }));
            Console.ReadLine();
        }
    }
}
