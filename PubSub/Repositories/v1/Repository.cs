using PubSub.Contracts.v1;
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
            catch (Exception)
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
                var result = await _client.PostAsync("https://localhost:44313/api/Product/Post/", content);
                if (result.IsSuccessStatusCode)
                {
                    var T = JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync());
                    return await Task.FromResult(T);
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
