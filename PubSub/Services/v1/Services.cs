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
            return await _repository.Post(entity);
        }
    }
}
