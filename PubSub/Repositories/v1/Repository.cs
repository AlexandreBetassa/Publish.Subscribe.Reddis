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
                using HttpResponseMessage response = await _client.GetAsync("");
                var responseContent = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonSerializer.Deserialize<List<T>?>(responseContent));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<T> GetOne(int id)
        {
            using HttpResponseMessage response = await _client.GetAsync($" /{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
            }
            else throw new Exception();
        }

        public async Task<T> Post(T entity)
        {
            try
            {
                var entityJson = JsonSerializer.Serialize(entity);
                var content = new StringContent(entityJson, Encoding.UTF8, "application/json");
                await _client.PostAsync("", content);
                return await Task.FromResult(entity);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
