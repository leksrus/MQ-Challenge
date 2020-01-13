using Api.Entitys;
using Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Api.Services
{
    public class FileManager : IFileManager
    {
        private readonly IOptions<MQConfig> _options;

        public FileManager(IOptions<MQConfig> options)
        {
            _options = options;
        }

        public bool CreateFile()
        {
            var input = _options.Value.FilesRoute + _options.Value.InputData;
            var output = _options.Value.FilesRoute + _options.Value.OutputData;
            try
            {
                File.Create(input).Dispose();
                File.Create(output).Dispose();

                return true;
            }
            catch (Exception)
            {
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

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
