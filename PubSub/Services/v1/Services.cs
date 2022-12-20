using PubSub.Contracts.v1;

namespace PubSub.Services.v1
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
            return _repository.GetAll().Result;
        }

        public T GetOne(int id)
        {
            return _repository.GetOne(id).Result;
        }

        public T Post(T entity)
        {
            return _repository.Post(entity).Result;
        }
    }
}
