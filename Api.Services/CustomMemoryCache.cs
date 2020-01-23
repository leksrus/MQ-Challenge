using Api.Entitys;
using Api.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CustomMemoryCache : ICustomMemoryCache
    {
        private readonly IMemoryCache _memoryCache;

        public CustomMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<Message> GetOrSetValueAsync(string key, Message message)
        {
            return await _memoryCache.GetOrCreateAsync(key, entry =>
            {
                return Task.FromResult(message);
            });
        }

        public void RemoveValue(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
