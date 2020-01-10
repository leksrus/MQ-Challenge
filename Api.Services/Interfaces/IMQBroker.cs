using Api.Entitys;

namespace Api.Services.Interfaces
{
    public interface IMQBroker
    {
        void PutMessage(Message message);

        Message GetMessage();
    }
}
