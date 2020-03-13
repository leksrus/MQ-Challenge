using Api.Entitys;
using Api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MqBrokerFile : IMQBroker
    {
        private readonly IFileManager _fileManager;
        private readonly ILogger<MqBrokerFile> _logger;

        public MqBrokerFile(IFileManager fileManager, ILogger<MqBrokerFile> logger)
        {
            _fileManager = fileManager;
            _logger = logger;
        }

        public async Task<List<Message>> GetMessages()
        {
            _logger.LogInformation("Getting messages");
            var lines = await _fileManager.GetAllLinesAsync();
            var messages = new List<Message>();

            foreach (var t in lines)
            {
                var message = new Message
                {
                    Product = new Product()
                };
                var line = t;
                var values = line.Split(";");
                message.HttpStatusCode = (HttpStatusCode)Convert.ToInt32(values[0]);
                message.Id = Convert.ToString(values[1]);
                message.Product.Id = Convert.ToInt32(values[2]);
                message.Product.Name = values[3];
                messages.Add(message);
            }
            _logger.LogInformation("Total messages: " + lines.Length);

            return messages;
        }

        public async Task<bool> PutMessage(Message message)
        {
            _logger.LogInformation("Putting message");

            var result = await _fileManager.SaveToFileAsync(MessageToString(message));

            if (result)
            {
                _logger.LogInformation("Message is in broker");

                return true;
            }
            _logger.LogInformation("Error putting message");

            return true;
        }

        public string MessageToString(object obj)
        {
            var sb = new StringBuilder();
            foreach (var prop in obj.GetType().GetProperties())
            {
                var propValue = prop.GetValue(obj, null);
                if (prop.PropertyType == typeof(HttpStatusCode)) continue;
                if (prop.PropertyType != typeof(string) && prop.PropertyType.IsClass)
                {
                    foreach (var subProp in prop.PropertyType.GetProperties())
                    {
                        var subPropValue = subProp.GetValue(propValue, null);
                        if (subPropValue != null && !string.IsNullOrEmpty(subPropValue.ToString()))
                            sb.Append(subPropValue + ";");
                    }
                }
                else
                {
                    if (propValue != null || !string.IsNullOrEmpty(propValue.ToString()))
                        sb.Append(propValue + ";");
                }
            }

            return sb.ToString();
        }
    }
}
