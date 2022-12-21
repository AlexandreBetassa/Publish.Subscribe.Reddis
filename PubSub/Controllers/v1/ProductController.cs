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
        public async Task<ActionResult<Product>> Post(Product product)
        {
            product = await _productService.Post(product);
            if (product == null) return BadRequest();
            await _productService.PublishRedis("Request received successfully. In process of data validation.\n" +
                "Number order: " + product.Id + "\n");
            return Ok(product);
        }

        [HttpGet("GetOne", Name = "GetOne")]
        public async  Task<ActionResult<Product>> Get(int id)
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
