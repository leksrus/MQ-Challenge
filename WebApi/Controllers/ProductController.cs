using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/api/[Controller]")]
        public IActionResult Get()
        {
            _logger.LogInformation("Get request /api/product");

            return Ok();
        }

        [HttpPost]
        [Route("/api/[Controller]")]
        public IActionResult Post()
        {
            _logger.LogInformation("Post request /api/product");

            return Ok();
        }

        [HttpPut]
        [Route("/api/[Controller]")]
        public IActionResult Put()
        {
            _logger.LogInformation("Put request /api/product");

            return Ok();
        }

        [HttpPatch]
        [Route("/api/[Controller]")]
        public IActionResult Patch()
        {
            _logger.LogInformation("Patch request /api/product");

            return Ok();
        }

        [HttpDelete]
        [Route("/api/[Controller]")]
        public IActionResult Delete()
        {
            _logger.LogInformation("Delete request /api/product");

            return Ok();
        }
    }
}