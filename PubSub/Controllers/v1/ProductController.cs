using Microsoft.AspNetCore.Mvc;
using PubSub.Contracts.v1;
using _Utils.Models.v1;

namespace PubSub.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> _productService;
        public ProductController(IService<Product> productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            var result = _productService.Post(product);
            if (result.IsFaulted) return BadRequest();
            return Ok(product);
        }

        [HttpGet("GetOne", Name = "GetOne")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetOne(id);
            if (result.IsFaulted) return BadRequest("Not Found");
            else return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _productService.GetAll();
            if (result.IsFaulted) return BadRequest(result);
            else return Ok(result);
        }
    }
}
