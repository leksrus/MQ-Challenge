using Api.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IMQBroker
    {
        Task<bool> PutMessage(Message message);

        Task<List<Message>> GetMessages();
    }
}
