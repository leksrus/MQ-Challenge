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

        public void CreateFiles()
        {
            var input = _options.Value.FilesRoute + _options.Value.InputData;
            var output = _options.Value.FilesRoute + _options.Value.OutputData;
            try
            {
                File.Create(input).Dispose();
                File.Create(output).Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteFiles()
        {
            var files = Directory.GetFiles(_options.Value.FilesRoute);
            try
            {
                foreach(var file in files)
                {
                    File.Delete(file);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
