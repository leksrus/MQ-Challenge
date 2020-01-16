using Api.Entitys;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("/api/[Controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICryptoManager _cryptoManager;
        private readonly IFileManager _fileManager;
        private readonly IMQBroker _mqBroker;

        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor httpContextAccessor, ICryptoManager cryptoManager, IFileManager fileManager,
                                  IMQBroker mqBroker)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _cryptoManager = cryptoManager;
            _fileManager = fileManager;
            _mqBroker = mqBroker;
        }

        [HttpGet("{idProduct}")]
        public IActionResult Get(int idProduct)
        {
            var url = nameof(HttpMethods.Get) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            var keyRequest = _cryptoManager.GetMD5Hash(url);
            _fileManager.CreateFile();
            var message = new Message
            {
                Id = keyRequest,
                Product = new Product
                {
                     Id = idProduct
                }
            };
            _mqBroker.PutMessage(message);

            _logger.LogInformation(url);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post()
        {
            var url = nameof(HttpMethods.Post) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            var keyRequest = _cryptoManager.GetMD5Hash(url);
            _logger.LogInformation(url);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put()
        {
            var url = nameof(HttpMethods.Put) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            var keyRequest = _cryptoManager.GetMD5Hash(url);
            _logger.LogInformation(url);

            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch()
        {
            var url = nameof(HttpMethods.Patch) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            var keyRequest = _cryptoManager.GetMD5Hash(url);
            _logger.LogInformation(url);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var url = nameof(HttpMethods.Delete) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            var keyRequest = _cryptoManager.GetMD5Hash(url);
            _logger.LogInformation(url);

            return Ok();
        }
    }
}