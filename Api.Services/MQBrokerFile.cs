﻿using Api.Entitys;
using Api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MQBrokerFile : IMQBroker
    {
        private readonly IOptions<MQConfig> _options;
        private readonly ILogger<MQBrokerFile> _logger;

        public MQBrokerFile(IOptions<MQConfig> options, ILogger<MQBrokerFile> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task<List<Message>> GetMessages()
        {
            _logger.LogInformation("Getting messages");
            var fileMQ = _options.Value.FilesRoute + _options.Value.InputData;
            var lines = await File.ReadAllLinesAsync(fileMQ);
            var messages = new List<Message>();

            for (int i = 0; i > lines.Length; i++)
            {
                var message = new Message();
                var line = lines[i];
                var values = line.Split(";");
                message.Id = Convert.ToString(values[0]);
                message.HttpStatusCode = (HttpStatusCode)Convert.ToInt32(values[1]);
                message.Product.Id = Convert.ToInt32(values[2]);
                message.Product.Name = values[3];
                messages.Add(message);
            }
            _logger.LogInformation("Total messages: " + lines.Length);

            return messages;
        }

        public async Task PutMessage(Message message)
        {
            _logger.LogInformation("Putting message");
            var fileMQ = _options.Value.FilesRoute + _options.Value.InputData;
            using var file = new StreamWriter(fileMQ);
            await file.WriteAsync(MessageToString(message));
            _logger.LogInformation("Message is in broker");
        }

        private string MessageToString(Message message)
        {
            var sb = new StringBuilder();
            sb.Append(message.Id + ";");
            sb.Append(message.Product.Id + ";");
            sb.Append(message.Product.Name + ";");

            return sb.ToString();
        }
    }
}
