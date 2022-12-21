using PubSub.Contracts.v1;
using PubSubApi.Services.v1;
using System.Text.Json;

namespace PubSub.Services.v1
{
    public class Services<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly RedisService _redis;
        public Services(IRepository<T> repository, RedisService redis)
        {
            _repository = repository;
            _redis = redis;
        }

        public async Task<List<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetOne(int id)
        {
            return await _repository.GetOne(id);
        }

        public async Task<T> Post(T entity)
        {
            await _redis.PublishRedis("Request sent to the central");
            var result = await _repository.Post(entity);
            await _redis.PublishRedisObject(JsonSerializer.Serialize(result));
            return result;
        }

        public async Task PublishRedis(int id)
        {
            await _redis.PublishRedis($"Request received successfully. In process of data validation. Number order: {id}");
        }
    }
}
