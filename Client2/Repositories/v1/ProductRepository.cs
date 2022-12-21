using Client2.Models.v1;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client2.Repositories.v1
{
    internal class ProductRepository
    {
        private HttpClient _client;
        public ProductRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool>Update(Product product)
        {
            var jsonText = JsonSerializer.Serialize(product);
            var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("https://localhost:44313/api/Product", content);
            if (response.IsSuccessStatusCode) return true;
            else return false;
        }
    }
}
