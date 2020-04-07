using Api.Entitys;
using Api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FileManager : IFileManager
    {
        private readonly ILogger<FileManager> _logger;

        public FileManager(ILogger<FileManager> logger)
        {
            _logger = logger;
        }

        public void CreateFile(string fileName, string filesRoute)
        {
            var completeRoute = Path.Combine(filesRoute, fileName);

            try
            {
                if (!Directory.Exists(filesRoute))
                {
                    Directory.CreateDirectory(filesRoute);
                    _logger.LogInformation("Directory created");
                }

                File.Create(completeRoute).Dispose();
                _logger.LogInformation("File created");
            }
            catch (Exception ex)
            {
                _logger.LogError("File creation failed " + ex.Message);
            }
        }

        public void DeleteFiles(string filesRoute)
        {
            if (!Directory.Exists(filesRoute))
            {
                _logger.LogInformation("Files delete failed. Route not exist");
                return;
            }

            var files = GetFiles(filesRoute);
            try
            {
                foreach (var file in files)
                {
                    File.Delete(file);
                }

                _logger.LogInformation("All files deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError("Files delete failed " + ex.Message);
            }
        }

        public string[] GetFiles(string fileRoute)
        {
            return Directory.GetFiles(fileRoute);
        }

        public async Task<bool> SaveToFileAsync(string message, string fileName, string filesRoute)
        {
            if (!Directory.Exists(filesRoute))
            {
                _logger.LogInformation("Save file failed. Route not exist");

                return false;
            }

            var fileMq = Path.Combine(filesRoute, fileName);
            _logger.LogInformation("Saving message to file");

            try
            {
                await using var file = new StreamWriter(fileMq, true);
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

        public async Task<string[]> GetAllLinesAsync(string fileName, string filesRoute)
        {
            _logger.LogInformation("Getting text");
            var fileMq = Path.Combine(filesRoute, fileName);
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