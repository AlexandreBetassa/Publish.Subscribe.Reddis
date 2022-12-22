using StackExchange.Redis;

namespace PubSubApi.Services.v1
{
    public class RedisService
    {
        private readonly ISubscriber _sub;
        public RedisService(IConnectionMultiplexer conn)
        {
            _sub = conn.GetSubscriber();
        }

        public async Task PublishRedis(string message)
        {
            await _sub.PublishAsync("Channel1", message, CommandFlags.FireAndForget);
        }

        public async Task PublishRedisObject(string message)
        {
            await _sub.PublishAsync("Channel2", message, CommandFlags.FireAndForget);
        }
    }
}
