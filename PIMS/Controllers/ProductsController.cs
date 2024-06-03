using Microsoft.AspNetCore.Mvc;
using ServicesContracts;
using ServicesContracts.Model;
using System;
using System.Threading.Tasks;

namespace Assignment_1.Controllers
{
    [Route("/products")]
    public class ProductsController : Controller
    {
        private readonly IProducts _products;

        public ProductsController(IProducts products)
        {
            _products = products;
        }

        [HttpGet("/")]
        public async Task<string> Home()
        {
            return "Hello World";
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProducts([FromBody] ProductsModel product)
        {
            try
            {
                var result = await _products.AddProducts(product);
                if (result > 0)
                {
                    return Ok("Product added");
                }
                return BadRequest("Product not added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var result = await _products.GetProducts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteProduct([FromBody] int Id)
        {
            try
            {
                var result = await _products.DeleteProduct(Id);
                if (result)
                {
                    return Ok("Product deleted successfully");
                }
                return NotFound("Product not found");
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
