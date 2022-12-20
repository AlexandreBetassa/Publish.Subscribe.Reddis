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
        public ActionResult<Product> Post(Product product)
        {
            var result = _productService.Post(product);
            if (result == null) return BadRequest();
            return Ok(product);
        }

        [HttpGet("GetOne", Name = "GetOne")]
        public ActionResult<Product> Get(int id)
        {
            var result = _productService.GetOne(id);
            if (result == null) return BadRequest("Not Found");
            else return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            var result = _productService.GetAll();
            if (result == null) return BadRequest(result);
            else return Ok(result);
        }
    }
}
