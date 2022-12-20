using DatabaseAPI.Context.v1;
using DatabaseAPI.Contracts.v1;
using Microsoft.Data.SqlClient;

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
            return _db.Set<T>().ToList();
        }

        public async Task<T> GetOneAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T> PostAsync(T entity)
        {
            try
            {
                _db.Set<T>().AddAsync(entity);
                _db.SaveChanges();
            }

            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            return entity;
        }
    }
}
