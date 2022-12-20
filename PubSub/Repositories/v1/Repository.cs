using PubSub.Contracts.v1;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace PubSub.Repositories.v1
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HttpClient _client;
        public Repository(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<T>?> GetAll()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("https://localhost:44313/api/Product/GetAll");
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<T>>(responseContent);
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<T> GetOne(int id)
        {
            using HttpResponseMessage response = await _client.GetAsync($"https://localhost:44313/api/Product/GetOne?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseContent);
            }
            else throw new Exception();
        }

        public async Task<T> Post(T entity)
        {
            try
            {
                var entityJson = JsonSerializer.Serialize(entity);
                var content = new StringContent(entityJson, Encoding.UTF8, "application/json");
                await _client.PostAsync("https://localhost:44313/api/Product/Post/", content);
                return entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
