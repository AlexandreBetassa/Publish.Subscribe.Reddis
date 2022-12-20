using DatabaseAPI.Contracts.v1;

namespace DatabaseAPI.IService.v1
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
            var list = await _repository.GetAll();
            return await Task.FromResult(list);
        }

        public async Task<T> GetOne(int id)
        {
            return await Task.FromResult(await _repository.GetOne(id));
        }

        public async Task<T> PostAsync(T entity)
        {
            return await Task.FromResult(await _repository.PostAsync(entity));
        }
    }
}
