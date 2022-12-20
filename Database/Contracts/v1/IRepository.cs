namespace DatabaseAPI.Contracts.v1
{
    public interface IRepository<T> where T : class
    {
        T PostAsync(T entity);
        T GetOne(int id);
        List<T> GetAll();
    }
}
