using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Api.Entitys;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class FileManagerTest
    {
        private readonly IMock<ILogger<FileManager>> _loggerMock;
        private const string FileRoute = @"TestMQFiles\";
        private readonly string _completeRoute;
        private const string InputData = "MQBrokerInputDataTest.csv";
        private const string OutputData = "MQBrokerOutputDataTest.csv";


        public FileManagerTest()
        {
            _loggerMock = new Mock<ILogger<FileManager>>();
            _completeRoute = Path.Combine(Directory.GetCurrentDirectory(), FileRoute);
        }

        [Fact]
        public void FileCreationCheck()
        {
            var mqBroker = new FileManager(_loggerMock.Object);
            mqBroker.CreateFile(InputData, _completeRoute);
            mqBroker.CreateFile(OutputData, _completeRoute);
            var isExistInputFile = File.Exists(Path.Combine(_completeRoute, InputData));
            var isExistOutputFileName = File.Exists(Path.Combine(_completeRoute, OutputData));

            var result = isExistInputFile && isExistOutputFileName;

            Assert.True(result);
        }

        [Fact]
        public void FileDeleteCheck()
        {
            var fileManagerMock = Mock.Of<IFileManager>();
            Mock.Get(fileManagerMock).Setup(x => x.GetFiles(_completeRoute)).Returns(() =>
                new[] {InputData, OutputData}
            );
            var mqBroker = new FileManager(_loggerMock.Object);
            mqBroker.DeleteFiles(_completeRoute);
            var inputFileName = File.Exists(Path.Combine(_completeRoute, InputData));
            var outputFileName = File.Exists(Path.Combine(_completeRoute, OutputData));

            var result = !inputFileName && !outputFileName;

            Assert.True(result);
        }

        [Fact]
        public async Task WriteToFile()
        {
            var mqBroker = new FileManager(_loggerMock.Object);
            var message = "Test message";
            mqBroker.CreateFile(InputData, _completeRoute);
            var isFileCreated = await mqBroker.SaveToFileAsync(message, InputData, _completeRoute);
            var lines = await File.ReadAllLinesAsync(Path.Combine(_completeRoute, InputData));

            Assert.True(isFileCreated);
            Assert.Equal(message, lines[0]);
        }

        [Fact]
        public async Task ReadFromFile()
        {
            var mqBroker = new FileManager(_loggerMock.Object);
            mqBroker.CreateFile(OutputData, _completeRoute);
            await using var file = new StreamWriter(Path.Combine(_completeRoute, OutputData), true);
            var message = "Test message";
            await file.WriteLineAsync(message);
            file.Close();
            var lines = await mqBroker.GetAllLinesAsync(OutputData, _completeRoute);

            Assert.Equal(message, lines[0]);
        }
    }
}