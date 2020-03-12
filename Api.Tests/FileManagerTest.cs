using System.IO;
using Api.Entitys;
using Api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class FileManagerTest
    {
        private readonly IMock<ILogger<FileManager>> _loggerMock;
        private const string FileRoute = @"C:\Users\okotylev\Documents\MQ Files\";
        private const string ImputData = "MQBrokerInputDataTest.csv";
        private const string OutputData = "MQBrokerOutputDataTest.csv";


        public FileManagerTest()
        {
            _loggerMock = new Mock<ILogger<FileManager>>();
        }

        [Fact]
        public void FileCreationCheck()
        {
            var mqConfig = new MQConfig
            {
                FilesRoute = FileRoute,
                InputData = ImputData,
                OutputData = OutputData
            };
            var optionsMock = new Mock<IOptions<MQConfig>>();
            optionsMock.Setup(ap => ap.Value).Returns(mqConfig);
            var mqBroker = new FileManager(optionsMock.Object, _loggerMock.Object);
            var createFileResult = mqBroker.CreateFile();
            var inputFileName = Path.GetFileName(optionsMock.Object.Value.FilesRoute + optionsMock.Object.Value.InputData);
            var outputFileName = Path.GetFileName(optionsMock.Object.Value.FilesRoute + optionsMock.Object.Value.OutputData);

            var result = createFileResult && !string.IsNullOrEmpty(inputFileName) && !string.IsNullOrEmpty(outputFileName);

            Assert.True(result);
        }
    }
}
