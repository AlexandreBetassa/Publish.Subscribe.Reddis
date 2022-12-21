using StackExchange.Redis;

namespace DatabaseAPI.Service.v1
{
    public class RedisService
    {
        private readonly ISubscriber _sub;
        public RedisService(IConnectionMultiplexer conn)
        {
            _sub = conn.GetSubscriber();
        }

        public async Task Publish(string message)
        {
            await _sub.PublishAsync("Channel1", message, CommandFlags.FireAndForget);
        }
    }
}
