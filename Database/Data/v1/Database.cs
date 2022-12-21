using DatabaseAPI.Context.v1;
using DatabaseAPI.Contracts.v1;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAPI.Data.v1
{
    public class Database<T> : IDatabase<T> where T : class
    {
        private readonly AppDbContext _db;
        public Database(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return  await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetOneAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T> PostAsync(T entity)
        {

            var result = await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return await Task.FromResult(result as T);
        }
    }
}
