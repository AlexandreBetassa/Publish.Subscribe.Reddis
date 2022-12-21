using Client2.Enum.v1;
using System.Text.Json.Serialization;

namespace Client2.Models.v1
{
    public class Product
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Value")]
        public double Value { get; set; }
        [JsonPropertyName("Cpf")]
        public string Cpf { get; set; }
        [JsonPropertyName("CreditCard")]
        public string CreditCard { get; set; }

        [JsonPropertyName("Status")]
        public StatusEnum Status { get; set; }
    }
}
