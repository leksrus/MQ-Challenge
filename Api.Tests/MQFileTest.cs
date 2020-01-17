using Api.Entitys;
using Api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests
{
    public class MQFileTest
    {
        private const string _idGet = "2";
        private const string _idPut = "2";

        [Fact]
        public async Task PutGetMessageToMQ()
        {
            var config = SetMqConfig();
            var optMock = new Mock<IOptions<MQConfig>>();
            var logMqMock = new Mock<ILogger<MQBrokerFile>>();
            optMock.Setup(ap => ap.Value).Returns(config);
            var mqBroker = new MQBrokerFile(optMock.Object, logMqMock.Object);


            var msg = new Message
            {
                Id = _idGet,
                Product = new Product
                {
                    Id = 1
                }
            };
            var result = await mqBroker.PutMessage(msg);
            Assert.True(result);
        }

        private MQConfig SetMqConfig()
        {
            return new MQConfig
            {
                FilesRoute = "C:\\Users\\okotylev\\Documents\\MQ Files\\",
                InputData = "MQBrokerInputData.csv",
                OutputData = "MQBrokerOutputData.csv"
            };
        }
    }
}
