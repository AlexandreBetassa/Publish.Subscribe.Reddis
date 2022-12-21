using Client2.Enum.v1;
using Client2.Models.v1;
using Client2.Services.v1;
using StackExchange.Redis;
using System;
using System.Text.Json;

namespace Client2
{
    public class Program
    {
        private const string RedisConnectionString = "localhost:6379";
        private static ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel = "Channel1";
        private const string Channel2 = "Channel2";
        static ISubscriber pubsub = connection.GetSubscriber();


        static void Main(string[] args)
        {
            Console.WriteLine("Status Order");
            Product product;
            pubsub.Subscribe(Channel2, (channel, message) =>
            {
                product = JsonSerializer.Deserialize<Product>(message);
                pubsub.PublishAsync(Channel, "Data received for validation.", CommandFlags.FireAndForget);
                CheckData(product);
            });

            pubsub.Subscribe(Channel, (channel, message) =>
            {
                Console.Write($"\n{message}");
            });
            Console.ReadLine();
        }

        static void CheckData(Product product)
        {
            if (!ValidatorServices.ValidateCpf(product.Cpf)) InvalidCpf(product);
            else if (!ValidatorServices.ValidateCreditCard(product.CreditCard)) InvalidCc(product);
            else Approved(product);
        }

        static void InvalidCpf(Product product)
        {
            product.Status = StatusEnum.Refused_Invalid_Cpf;
            pubsub.PublishAsync(Channel, "Refused: Invalid CPF", CommandFlags.FireAndForget);
        }

        static void InvalidCc(Product product)
        {
            product.Status = StatusEnum.Refused_Invalid_CreditCard;
            pubsub.PublishAsync(Channel, "Refused: Invalid Credit Card", CommandFlags.FireAndForget);
        }

        static void Approved(Product product)
        {
            product.Status = StatusEnum.Approved;
            pubsub.PublishAsync(Channel, "Approved", CommandFlags.FireAndForget);
        }
    }
}
