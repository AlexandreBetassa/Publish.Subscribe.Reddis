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

        public List<T> GetAll()
        {
            return _db.GetAllAsync();
        }

        public T GetOne(int id)
        {
            return _db.GetOneAsync(id);
        }

        public T PostAsync(T entity)
        {
            return _db.PostAsync(entity);
        }
    }
}
