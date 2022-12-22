using DatabaseAPI.Contracts.v1;
using DatabaseAPI.Service.v1;
using System.Text.Json;

namespace DatabaseAPI.IService.v1
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

        public async Task<T> PostAsync(T entity)
        {
            try
            {
                await Task.Run(async () => await _redis.Publish("Order received at the central, in processing."));
                var result = await _repository.PostAsync(entity);
                return result;
            }
            catch (Exception)
            {
                await Task.Run(async () => await _redis.Publish("Failed to process the request. Try again."));
                throw;
            }
        }

        public async Task<T> PutAsync(T entity)
        {
            return await _repository.PutAsync(entity);
        }
    }
}
