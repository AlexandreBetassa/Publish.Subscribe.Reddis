using _Utils.Contracts.v1;
using _Utils.Enum.v1;
using System.ComponentModel.DataAnnotations;

namespace _Utils.Models.v1
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }
        public double Value { get; set; }
        public string Cpf { get; set; }
        public string CreditCard { get; set; }
        public StatusEnum Status { get; set; }
    }
}
