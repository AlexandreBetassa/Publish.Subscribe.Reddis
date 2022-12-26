namespace PubSub.Contracts.v1
{
    public interface IRepository<T> where T : class
    {
        Task Post(T entity);
        Task<List<T>> GetAll();
        Task<T> GetOne(int id);
        Task<string> GetTest();
    }
}
