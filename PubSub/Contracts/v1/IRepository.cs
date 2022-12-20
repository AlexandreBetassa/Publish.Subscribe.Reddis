using Microsoft.AspNetCore.Mvc;

namespace PubSub.Contracts.v1
{
    public interface IRepository<T> where T : class
    {
        Task<T> Post(T entity);
        Task<List<T>> GetAll();
        Task<T> GetOne(int id);
    }
}
