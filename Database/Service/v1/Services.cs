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

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetOne(int id)
        {
            return _repository.GetOne(id);
        }

        public T PostAsync(T entity)
        {
            _sub.PublishAsync("Channel1", "Order in Process", CommandFlags.FireAndForget);
            try
            {
                return _repository.PostAsync(entity);
                
            }
            catch (Exception)
            {
                _sub.PublishAsync("Channel1", "Failed to process the request. Try again.", CommandFlags.FireAndForget);
                throw;
            }
        }
    }
}
