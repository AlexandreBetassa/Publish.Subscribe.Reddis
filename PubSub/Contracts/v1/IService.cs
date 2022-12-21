namespace PubSub.Contracts.v1
{
    public interface IService<T> where T : class
    {
        T Post(T entity);
        List<T> GetAll();
        T GetOne(int id);
        void PublishRedis(string message);
    }
}
