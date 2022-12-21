using PubSub.Contracts.v1;
using StackExchange.Redis;

namespace PubSub.Services.v1
{
    public class Services<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly ISubscriber _sub;
        public Services(IRepository<T> repository, IConnectionMultiplexer connection)
        {
            _repository = repository;
            _sub = connection.GetSubscriber();
        }

        public async Task<List<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetOne(int id)
        {
            return await _repository.GetOne(id);
        }

        public async Task PublishRedis(string message)
        {
            await _sub.PublishAsync("Channel1", message, CommandFlags.FireAndForget);
        }

        public async Task<T> Post(T entity)
        {
            await PublishRedis("Request sent to the central");
            var result = await _repository.Post(entity);
            return result;
        }
    }
}
