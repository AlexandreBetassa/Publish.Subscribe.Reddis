using PubSub.Contracts.v1;
using PubSubApi.Contracts.v1;
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

        public List<T> GetAll()
        {
            return _repository.GetAll().Result;
        }

        public T GetOne(int id)
        {
            return _repository.GetOne(id).Result;
        }

        public void PublishRedis(string message)
        {
            _sub.PublishAsync("Channel1", message, CommandFlags.FireAndForget);

        }

        public T Post(T entity)
        {
            PublishRedis("Request sent to the central");
            var result = _repository.Post(entity).Result;
            return result;
        }
    }
}
