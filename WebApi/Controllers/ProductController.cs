using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("/api/[Controller]")]
        public IActionResult Get()
        {
            var url = nameof(HttpMethods.Get) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            _logger.LogInformation(url);

            return Ok();
        }

        [HttpPost]
        [Route("/api/[Controller]")]
        public IActionResult Post()
        {
            var url = nameof(HttpMethods.Post) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            _logger.LogInformation(url);

            return Ok();
        }

        [HttpPut]
        [Route("/api/[Controller]")]
        public IActionResult Put()
        {
            var url = nameof(HttpMethods.Put) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            _logger.LogInformation(url);

            return Ok();
        }

        [HttpPatch]
        [Route("/api/[Controller]")]
        public IActionResult Patch()
        {
            var url = nameof(HttpMethods.Patch) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            _logger.LogInformation(url);

            return Ok();
        }

        [HttpDelete]
        [Route("/api/[Controller]")]
        public IActionResult Delete()
        {
            var url = nameof(HttpMethods.Delete) + " Request " + UrlCustomHelperExtensions.AbsoluteUrl(_httpContextAccessor);
            _logger.LogInformation(url);

            return Ok();
        }
    }
}