using Api.Entitys;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests
{
    public class MQFileTest
    {
        private readonly IMock<IFileManager> _fileManagerMock;
        private readonly IMock<ILogger<MQBrokerFile>> _loggerMock;
        private const string _idGet = "ZaQy256UWxiqKRATOfqdpw==";
        private const string _idPost = "UIsUuc5RHRk4BDlkh3U5jg==";
        private const string _idPut = "QChzkiZu4ybYQxWatg8ZkQ==";
        private const string _idPatch = "EYvjL67ZtW/yJ2KBnz1t6A==";
        private const string _idDelete = "E5bZmA6VnKbgNMUmNIzrvA==";

        public MQFileTest()
        {
            _fileManagerMock = new Mock<IFileManager>();
            _loggerMock = new Mock<ILogger<MQBrokerFile>>();
        }

        [Fact]
        public void ParseGetMessage()
        {
            var mqBroker = new MQBrokerFile(_fileManagerMock.Object, _loggerMock.Object);
            var msg = new Message
            {
                Id = _idGet,
                Product = new Product
                {
                    Id = 1
                }
            };
            var result = mqBroker.MessageToString(msg);
            Assert.Equal($"{_idGet};1;", result);
        }

        [Fact]
        public void ParseDeleteMessage()
        {
            var mqBroker = new MQBrokerFile(_fileManagerMock.Object, _loggerMock.Object);
            var msg = new Message
            {
                Id = _idDelete,
                Product = new Product
                {
                    Id = 1
                }
            };
            var result = mqBroker.MessageToString(msg);
            Assert.Equal($"{_idDelete};1;", result);
        }

        [Fact]
        public void ParsePostMessage()
        {
            var mqBroker = new MQBrokerFile(_fileManagerMock.Object, _loggerMock.Object);
            var msg = new Message
            {
                Id = _idPost,
                Product = new Product
                {
                    Id = 1,
                    Name = "T-Shirt"
                }
            };
            var result = mqBroker.MessageToString(msg);
            Assert.Equal($"{_idPost};1;T-Shirt;", result);
        }

        [Fact]
        public void ParsePutMessage()
        {
            var mqBroker = new MQBrokerFile(_fileManagerMock.Object, _loggerMock.Object);
            var msg = new Message
            {
                Id = _idPut,
                Product = new Product
                {
                    Id = 1,
                    Name = "T-Shirt"
                }
            };
            var result = mqBroker.MessageToString(msg);
            Assert.Equal($"{_idPut};1;T-Shirt;", result);
        }

        [Fact]
        public void ParsePatchMessage()
        {
            var mqBroker = new MQBrokerFile(_fileManagerMock.Object, _loggerMock.Object);
            var msg = new Message
            {
                Id = _idPatch,
                Product = new Product
                {
                    Id = 1,
                    Name = "T-Shirt"
                }
            };
            var result = mqBroker.MessageToString(msg);
            Assert.Equal($"{_idPatch};1;T-Shirt;", result);
        }

        [Fact]
        public async Task GetMessageOK()
        {
            var mockFileManager = _fileManagerMock as Mock<IFileManager>;
            mockFileManager.Setup(m => m.GetAllLinesAsync()).ReturnsAsync(GetResponseMessage());
            var msg = new Message
            {
                Id = _idGet,
                HttpStatusCode = HttpStatusCode.OK,
                Product = new Product
                {
                    Id = 1,
                    Name = "T-Shirt"
                }
            };
            var mqBroker = new MQBrokerFile(_fileManagerMock.Object, _loggerMock.Object);
            var message = await mqBroker.GetMessages();

            var expected = JsonSerializer.Serialize(msg);
            var actual = JsonSerializer.Serialize(message.First());

            Assert.Equal(expected, actual);
        }

        private string[] GetResponseMessage()
        {
            return new string[] { $"200;{_idGet};1;T-Shirt;" };
        }

        [Fact]
        public async Task GetMessageEmpty()
        {
            var mockFileManager = _fileManagerMock as Mock<IFileManager>;
            mockFileManager.Setup(m => m.GetAllLinesAsync()).ReturnsAsync(GetResponseMessageEmpty());
            var msg = new Message
            {
                Id = _idGet,
                HttpStatusCode = HttpStatusCode.OK,
                Product = new Product
                {
                    Id = 0,
                    Name = string.Empty
                }
            };
            var mqBroker = new MQBrokerFile(_fileManagerMock.Object, _loggerMock.Object);
            var message = await mqBroker.GetMessages();

            var expected = JsonSerializer.Serialize(msg);
            var actual = JsonSerializer.Serialize(message.First());

            Assert.Equal(expected, actual);
        }

        private string[] GetResponseMessageEmpty()
        {
            return new string[] { $"200;{_idGet};0;;" };
        }

        [Fact]
        public async Task PostMessage()
        {
            var mockFileManager = _fileManagerMock as Mock<IFileManager>;
            mockFileManager.Setup(m => m.GetAllLinesAsync()).ReturnsAsync(PostResponseMessage());
            var msg = new Message
            {
                Id = _idPost,
                HttpStatusCode = HttpStatusCode.Created,
                Product = new Product
                {
                    Id = 1,
                    Name = "T-Shirt"
                }
            };
            var mqBroker = new MQBrokerFile(_fileManagerMock.Object, _loggerMock.Object);
            var message = await mqBroker.GetMessages();

            var expected = JsonSerializer.Serialize(msg);
            var actual = JsonSerializer.Serialize(message.First());

            Assert.Equal(expected, actual);
        }

        private string[] PostResponseMessage()
        {
            return new string[] { $"201;{_idPost};1;T-Shirt;" };
        }
    }
}
