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
            return await _serviceProduct.GetAll();
        }

        [HttpGet("GetOne", Name = "GetOne")]
        public async Task<ActionResult<Product>> GetOneDb(int id)
        {
            var result = _serviceProduct.GetOne(id);
            return await result;
        }

        [HttpPost("Post", Name = "Post")]
        public async Task<ActionResult<Product>> PostDb([FromBody] Product product)
        {
            product = await _serviceProduct.PostAsync(product);
            var result = CreatedAtRoute("GetOne", new { id = product.Id }, product);
            return result;
        }


        [HttpPut]
        public async Task<ActionResult<Product>> Put([FromBody] Product product)
        {
            await Task.Run(() => _serviceProduct.PutAsync(product));
            return CreatedAtRoute("GetOne", new { id = product.Id }, product);
        }

    }
}
