using System.IO;
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
        private readonly MQConfig _mqConfig;
        private const string FileRoute = @"TestMQFiles\";
        private const string ImputData = "MQBrokerInputDataTest.csv";
        private const string OutputData = "MQBrokerOutputDataTest.csv";


        public FileManagerTest()
        {
            _loggerMock = new Mock<ILogger<FileManager>>();
            _mqConfig = new MQConfig
            {
                FilesRoute = Path.Combine(Directory.GetCurrentDirectory(), FileRoute),
                InputData = ImputData,
                OutputData = OutputData
            };
        }

        [Fact]
        public void FileCreationCheck()
        {
            var optionsMock = SetIOptionMock();
            var mqBroker = new FileManager(optionsMock.Object, _loggerMock.Object);
            var createFileResult = mqBroker.CreateFile();
            var isExistInputFile = File.Exists(Path.Combine(optionsMock.Object.Value.FilesRoute, optionsMock.Object.Value.InputData));
            var isExistOutputFileName = File.Exists(Path.Combine(optionsMock.Object.Value.FilesRoute, optionsMock.Object.Value.OutputData));

            var result = createFileResult && isExistInputFile && isExistOutputFileName;

            Assert.True(result);
        }

        [Fact]
        public void FileDeleteCheck()
        {
            var optionsMock = SetIOptionMock();
            var fileManagerMock = Mock.Of<IFileManager>();
            Mock.Get(fileManagerMock).Setup(x => x.GetFiles()).Returns(() =>

                new[] { ImputData, OutputData }
            );
            var mqBroker = new FileManager(optionsMock.Object, _loggerMock.Object);
            var deleteFileResult = mqBroker.DeleteFiles();
            var inputFileName = File.Exists(Path.Combine(optionsMock.Object.Value.FilesRoute, optionsMock.Object.Value.InputData));
            var outputFileName = File.Exists(Path.Combine(optionsMock.Object.Value.FilesRoute, optionsMock.Object.Value.OutputData));

            var result = deleteFileResult && !inputFileName && !outputFileName;

            Assert.True(result);
        }

        [Fact]
        public async Task WriteToFile()
        {
            var optionsMock = SetIOptionMock();
            var mqBroker = new FileManager(optionsMock.Object, _loggerMock.Object);
            var message = "Test message";
            var isFileCreated = await mqBroker.SaveToFileAsync(message);
            var lines = await File.ReadAllLinesAsync(Path.Combine(optionsMock.Object.Value.FilesRoute,
                optionsMock.Object.Value.InputData));

            Assert.True(isFileCreated);
            Assert.Equal(message, lines[0]);
        }

        [Fact]
        public async Task ReadFromFile()
        {
            var optionsMock = SetIOptionMock();
            var mqBroker = new FileManager(optionsMock.Object, _loggerMock.Object);
            await using var file = new StreamWriter(Path.Combine(optionsMock.Object.Value.FilesRoute, optionsMock.Object.Value.OutputData), true);
            var message = "Test message";
            await file.WriteLineAsync(message);
            file.Close();
            var lines = await mqBroker.GetAllLinesAsync();

            Assert.Equal(message, lines[0]);
        }

        private Mock<IOptions<MQConfig>> SetIOptionMock()
        {
            var optionsMock = new Mock<IOptions<MQConfig>>();
            optionsMock.Setup(ap => ap.Value).Returns(_mqConfig);

            return optionsMock;
        }
    }
}
