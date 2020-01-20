using Api.Entitys;
using Api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FileManager : IFileManager
    {
        private readonly IOptions<MQConfig> _options;
        private readonly ILogger<FileManager> _logger;

        public FileManager(IOptions<MQConfig> options, ILogger<FileManager> logger)
        {
            _options = options;
            _logger = logger;
        }

        public bool CreateFile()
        {
            var input = _options.Value.FilesRoute + _options.Value.InputData;
            var output = _options.Value.FilesRoute + _options.Value.OutputData;
            try
            {
                File.Create(input).Dispose();
                File.Create(output).Dispose();
                _logger.LogInformation("Files created");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Files creation failed " + ex.Message);

                return false;
            }
        }

        public bool DelteFiles()
        {
            var files = Directory.GetFiles(_options.Value.FilesRoute);
            try
            {
                foreach (var file in files)
                {
                    File.Delete(file);
                }
                _logger.LogInformation("All files deleted");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Files delete failed " + ex.Message);

                return false;
            }
        }

        public async Task<bool> SaveToFileAsync(string message)
        {
            var fileMQ = _options.Value.FilesRoute + _options.Value.InputData;
            _logger.LogInformation("Saving message to file");

            try
            {
                await using var file = new StreamWriter(fileMQ, true);
                await file.WriteLineAsync(message);
                file.Close();
                _logger.LogInformation("Message saved");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error saving to file: " + ex.Message);

                return false;
            }
        }

        public async Task<string[]> GetAllLinesAsync()
        {
            _logger.LogInformation("Getting text");
            var fileMQ = _options.Value.FilesRoute + _options.Value.InputData;
            try
            {
                var lines = await File.ReadAllLinesAsync(fileMQ);
                _logger.LogInformation("Get messages Ok");

                return lines;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Fail to get text: " + ex.Message);
               return new string [0];
            }



        }
    }
}
