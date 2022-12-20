using DatabaseAPI.Contracts.v1;
using DatabaseAPI.Models.v1;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> _serviceProduct;
        public ProductController(IService<Product> serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }

        [HttpGet("GetAll", Name = "GetAll")]
        public async Task<ActionResult<List<Product>>> GetDb()
        {
            var result = await _serviceProduct.GetAll();
            return await Task.FromResult(result);

        }

        [HttpGet("GetOne", Name = "GetOne")]
        public async Task<ActionResult<Product>> GetOneDb(int id)
        {
            var result = await _serviceProduct.GetOne(id);
            return await Task.FromResult(result);
        }

        [HttpPost("Post", Name = "Post")]
        public async Task<ActionResult<Product>> PostDb(Product product)
        {
            _serviceProduct.PostAsync(product);
            var result = CreatedAtRoute("GetOne", new { id = product.Id }, product);
            return await Task.FromResult(result);
        }
    }
}
