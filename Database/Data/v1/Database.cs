using DatabaseAPI.Context.v1;
using DatabaseAPI.Contracts.v1;
using Microsoft.Data.SqlClient;
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

        public List<T> GetAllAsync()
        {
            return _db.Set<T>().ToList();
        }

        public T GetOneAsync(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public T PostAsync(T entity)
        {

            _db.Set<T>().AddAsync(entity);
            _db.SaveChangesAsync();
            return entity;

        }
    }
}
