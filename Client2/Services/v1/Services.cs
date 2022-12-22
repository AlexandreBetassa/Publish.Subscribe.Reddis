using Client2.Enum.v1;
using Client2.Models.v1;
using Client2.Repositories.v1;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client2.Services.v1
{
    public static class Services
    {
        private const string RedisConnectionString = "localhost:6379";
        private static ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel = "Channel1";
        static ISubscriber pubsub = connection.GetSubscriber();

        public static bool ValidateCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool ValidateCreditCard(string number)
        {
            var cc = new CreditCardAttribute();
            if (cc.IsValid(number)) return true;
            else return false;
        }

        public static async Task CheckData(Product product)
        {
            if (!ValidateCpf(product.Cpf)) await Task.Run(async () => await InvalidCpf(product));
            else if (!ValidateCreditCard(product.CreditCard)) await Task.Run(async () => await InvalidCc(product));
            else await Task.Run(async () => await Approved(product));
        }

        public static async Task InvalidCpf(Product product)
        {
            product.Status = StatusEnum.Refused_Invalid_Cpf;

            await pubsub.PublishAsync(Channel,
                                      $"Refused: Invalid CPF. " +
                                      GetString(product),
                                      CommandFlags.FireAndForget);
            await Update(product);
        }

        public static async Task InvalidCc(Product product)
        {
            product.Status = StatusEnum.Refused_Invalid_CreditCard;

            await pubsub.PublishAsync(Channel,
                                      $"Refused: Invalid Credit Card. " +
                                      GetString(product),
                                      CommandFlags.FireAndForget);
            await Update(product);
        }

        public static async Task Approved(Product product)
        {
            product.Status = StatusEnum.Approved;

            await pubsub.PublishAsync(Channel,
                                      $"Approved. " +
                                      GetString(product),
                                      CommandFlags.FireAndForget);
            await Update(product);
        }

        static async Task Update(Product product)
        {
            var repository = new ProductRepository(new HttpClient());
            var result = await repository.Update(product);
            await PublishChannel(result);
        }

        public static async Task<Product> Post(Product product)
        {
            var repository = new ProductRepository(new HttpClient());
            return await repository.Post(product);
        }

        static async Task PublishChannel(bool result)
        {
            if (result) await pubsub.PublishAsync(Channel, "Database refresh success\n", CommandFlags.FireAndForget);
            else await pubsub.PublishAsync(Channel, "Database refresh fail\n", CommandFlags.FireAndForget);
        }

        static string GetString(Product product)
        {
            return $"Order number : {product.Id}. " +
                   $"Order CPF: {product.Cpf}. " +
                   $"Order Credit Card: {product.CreditCard}";
        }
    }
}