using Api.Entitys;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface ICustomMemoryCache
    {
        Task<Message> GetOrSetValueAsync(string key, Message message);

        void RemoveValue(string key);
    }
}
