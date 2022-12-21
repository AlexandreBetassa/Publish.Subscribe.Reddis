using DatabaseAPI.Contracts.v1;
using StackExchange.Redis;

namespace DatabaseAPI.IService.v1
{
    public class Services<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly ISubscriber _sub;
        public Services(IRepository<T> repository, IConnectionMultiplexer conn)
        {
            _repository = repository;
            _sub = conn.GetSubscriber();
        }

        public async Task<List<T>> GetAll()
        {
            return  await _repository.GetAll();
        }

        public async Task<T> GetOne(int id)
        {
            return await _repository.GetOne(id);
        }

        public async Task<T> PostAsync(T entity)
        {
            await _sub.PublishAsync("Channel1", "Order in Process", CommandFlags.FireAndForget);
            try
            {
                return await _repository.PostAsync(entity);
                
            }
            catch (Exception)
            {
                await _sub.PublishAsync("Channel1", "Failed to process the request. Try again.", CommandFlags.FireAndForget);
                throw;
            }
        }
    }
}
