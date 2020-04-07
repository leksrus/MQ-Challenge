using Api.Entitys;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("/api/[Controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICryptoManager _cryptoManager;
        private readonly IMapper _mapper;
        private readonly IMQBroker _mqBroker;
        private readonly ICustomMemoryCache _customMemoryCache;

        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor httpContextAccessor, ICryptoManager cryptoManager, IMapper mapper,
                                  IMQBroker mqBroker, ICustomMemoryCache customMemoryCache)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _cryptoManager = cryptoManager;
            _mapper = mapper;
            _mqBroker = mqBroker;
            _customMemoryCache = customMemoryCache;
        }

        [HttpGet("{idProduct}")]
        public async Task<IActionResult> Get(int idProduct)
        {
            var url = nameof(HttpMethods.Get) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            var keyRequest = _cryptoManager.GetMD5Hash(url);
            var message = new Message
            {
                Id = keyRequest,
                Product = new Product
                {
                    Id = idProduct
                }
            };

            var putMsgResult = await _mqBroker.PutMessage(message);

            if(putMsgResult)
                return Ok();

            _logger.LogInformation(url);

            return BadRequest();
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

        [HttpDelete("{idProduct}")]
        public IActionResult Delete(int idProduct)
        {
            var url = nameof(HttpMethods.Delete) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            var keyRequest = _cryptoManager.GetMD5Hash(url);
            _logger.LogInformation(url);

            return Ok();
        }
    }
}