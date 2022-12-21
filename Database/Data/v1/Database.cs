using DatabaseAPI.Context.v1;
using DatabaseAPI.Contracts.v1;

namespace DatabaseAPI.Data.v1
{
    public class Database<T> : IDatabase<T> where T : class
    {
        private readonly AppDbContext _db;
        public Database(AppDbContext db)
        {
            _db = db;
        }

        public List<T> GetAllAsync()
        {
            return _db.Set<T>().ToList();
        }

        public T GetOneAsync(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public async Task<T> PostAsync(T entity)
        {

            var result = _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return await Task.FromResult(result as T);
        }
    }
}
