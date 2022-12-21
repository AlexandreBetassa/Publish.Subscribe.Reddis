namespace DatabaseAPI.Contracts.v1
{
    public interface IDatabase<T> where T : class
    {
        Task<T> PostAsync(T entity);
        Task<T> GetOneAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> PutAsync(T entity);
    }
}
