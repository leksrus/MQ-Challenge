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
                if (!Directory.Exists(_options.Value.FilesRoute))
                {
                    _logger.LogInformation("Files creation failed. Route not exist");

                    return false;
                }

                Directory.CreateDirectory(_options.Value.FilesRoute);
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

        public bool DeleteFiles()
        {
            if (!Directory.Exists(_options.Value.FilesRoute))
            {
                _logger.LogInformation("Files delete failed. Route not exist");

                return false;
            }

            var files = GetFiles();
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

        public string[] GetFiles()
        {
            return Directory.GetFiles(_options.Value.FilesRoute);
        }

        public async Task<bool> SaveToFileAsync(string message)
        {
            if (!Directory.Exists(_options.Value.FilesRoute))
            {
                _logger.LogInformation("Save file failed. Route not exist");

                return false;
            }

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
            var fileMq = _options.Value.FilesRoute + _options.Value.OutputData;
            try
            {
                var lines = await File.ReadAllLinesAsync(fileMq);
                _logger.LogInformation("Get messages Ok");

                return lines;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Fail to get text: " + ex.Message);
                return new string[0];
            }



        }
    }
}
