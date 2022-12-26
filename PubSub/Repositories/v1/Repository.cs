using PubSub.Contracts.v1;
using PubSubApi.Services.v1;
using System.Text.Json;

namespace PubSub.Repositories.v1
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HttpClient _client;
        private readonly RedisService _redis;

        public Repository(HttpClient client, RedisService redis)
        {
            _client = client;
            _redis = redis;
        }

        public async Task<List<T>?> GetAll()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("http://DESKTOP-49RHHLK:44313/api/Product/GetAll");
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
            using HttpResponseMessage response = await _client.GetAsync($"http://DESKTOP-49RHHLK:44313/api/Product/GetOne?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseContent);
            }
            else throw new Exception();
        }

        public async Task Post(T entity)
        {
            await Task.Run(async () => await _redis.PublishRedis("Request sent to the central"));
            await Task.Run(async () => await _redis.PublishRedisObject(JsonSerializer.Serialize(entity)));
        }
    }
}
