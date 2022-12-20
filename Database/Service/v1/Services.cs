using DatabaseAPI.Contracts.v1;
using StackExchange.Redis;

namespace DatabaseAPI.IService.v1
{
    public class Services<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        public Services(IRepository<T> repository)
        {
            _repository = repository;
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetOne(int id)
        {
            return _repository.GetOne(id);
        }

        public void OrderReceived()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            ISubscriber sub = redis.GetSubscriber();
            sub.Publish("Channel1", "Order received, status: Pending", CommandFlags.FireAndForget);
        }

        public T PostAsync(T entity)
        {
            return _repository.PostAsync(entity);
        }
    }
}
