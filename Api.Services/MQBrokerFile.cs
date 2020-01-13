using Api.Entitys;
using Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Api.Services
{
    public class MQBrokerFile : IMQBroker
    {
        private readonly IOptions<MQConfig> _options;

        public MQBrokerFile(IOptions<MQConfig> options)
        {
            _options = options;
        }

        public Message GetMessage()
        {
            throw new NotImplementedException();
        }

        public void PutMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
