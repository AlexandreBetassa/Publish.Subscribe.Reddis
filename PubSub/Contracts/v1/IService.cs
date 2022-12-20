namespace PubSub.Contracts.v1
{
    public interface IService<T> where T : class
    {
        Task<T> Post(T entity);
        Task<List<T>> GetAll();
        Task<T> GetOne(int id);
    }
}
