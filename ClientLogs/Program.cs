using StackExchange.Redis;
using System;

namespace ClientLogs
{
    internal class Program
    {
        private const string RedisConnectionString = "localhost:6379";
        private static ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel = "Channel1";
        static void Main(string[] args)
        {
            Console.WriteLine("Status Order Client");
            var pubsub = connection.GetSubscriber();
            pubsub.Subscribe(Channel, (channel, message) => Console.Write("\nStatus: " + message));
            Console.ReadLine();
        }
    }
}
