using Microsoft.AspNetCore.Mvc;
using PubSub.Contracts.v1;
using PubSubApi.Models.v1;

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
        public async Task<IActionResult> Post(Product product)
        {
            await _productService.Post(product);
            return Ok("Order sent success");
        }

        [HttpGet("GetOne", Name = "GetOne")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var result = await _productService.GetOne(id);
            if (result == null) return BadRequest("Not Found");
            else return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {

            var result = await _productService.GetAll();
            if (result == null) return BadRequest(result);
            else return Ok(result);
        }
    }
}
