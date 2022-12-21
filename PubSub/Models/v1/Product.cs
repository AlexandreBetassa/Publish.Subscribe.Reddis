using PubSubApi.Enum.v1;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PubSubApi.Models.v1
{
    public class Product 
    {
        [Key]
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
