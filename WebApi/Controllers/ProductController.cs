using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("/api/[Controller]")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        [Route("/api/[Controller]")]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpPut]
        [Route("/api/[Controller]")]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpPatch]
        [Route("/api/[Controller]")]
        public IActionResult Patch()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("/api/[Controller]")]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}