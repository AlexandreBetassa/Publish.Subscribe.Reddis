using PubSub.Contracts.v1;

namespace PubSub.Models.v1
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Cpf { get; set; }
        public string CreditCard { get; set; }
    }
}
