using InventoryManagement.Server.Data;
using InventoryManagement.Server.Data.Models;
using InventoryManagement.Server.Models;
using InventoryManagement.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet(Name = "GetAllProducts")]
        public IActionResult Get()
        {
          return Ok(_productService.GetAllProducts());
        }
        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null.");
            }

            var createdProduct = _productService.CreateProduct(productDto);
            return CreatedAtRoute("GetProductById", new { id = createdProduct.ProductID }, createdProduct);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult Get(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDTO productDto)
        {
            if (productDto == null || id != productDto.ProductID)
            {
                return BadRequest("Product data is invalid.");
            }

            var updatedProduct = _productService.UpdateProduct(id, productDto);
            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
