namespace DatabaseAPI.Contracts.v1
{
    public interface IService<T> where T : class
    {
        Task<T> PostAsync(T entity);
        Task<T> GetOne(int id);
        Task<List<T>> GetAll();
    }
}
