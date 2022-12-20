using DatabaseAPI.Contracts.v1;

namespace DatabaseAPI.Repository.v1
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDatabase<T> _db;
        public Repository(IDatabase<T> db)
        {
            _db = db;
        }

        public async Task<List<T>> GetAll()
        {
            return await _db.GetAllAsync();
        }

        public async Task<T> GetOne(int id)
        {
            return await _db.GetOneAsync(id);
        }

        public async Task<T> PostAsync(T entity)
        {
            return await _db.PostAsync(entity);
        }
    }
}
