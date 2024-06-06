﻿using Microsoft.AspNetCore.Mvc;
using Services.Helper;
using ServicesContract.Model;
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
        private readonly ExportExcel _exportExcel;

        public ProductsController(IProducts products, ExportExcel exportExcel)
        {
            _products = products;
            _exportExcel = exportExcel;
        }

        [HttpGet("/")]
        public async Task<string> Home()
        {
            return "Hello World";
        }


        /// <summary>
        /// Endpoint to add a new product.
        /// </summary>
        /// <param name="product">The model containing product details.</param>
        /// <returns>Returns the result of adding the product.</returns>
       
        [HttpPost("add")]
        public async Task<IActionResult> AddProducts([FromBody] ProductsModel product)
        {
            try
            {
                var result = await _products.AddProducts(product);
                if (result)
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


        /// <summary>
        /// Endpoint to retrieve all products.
        /// </summary>
        /// <returns>Returns the list of all products.</returns>
       
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


        /// <summary>
        /// Endpoint to delete a product by ID.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <returns>Returns the result of deleting the product.</returns>
        
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
        {
            try
            {
                var result = await _products.DeleteProduct(productId);
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


        /// <summary>
        /// Endpoint to update a product.
        /// </summary>
        /// <param name="product">The model containing product details.</param>
        /// <returns>Returns the result of updating the product.</returns>
        
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductsModel product)
        {
            try
            {
                var result = await _products.UpdateProduct(product);
                if (result)
                {
                    return Ok("Product updated successfully");
                }
                return NotFound("Product not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Endpoint to create and download an Excel sheet of the database.
        /// </summary>
        /// <param name="excelExportModel">The model containing export parameters.</param>
        /// <returns>Returns the exported Excel sheet.</returns>
        
        [HttpPost("createExcel")]
        public async Task<IActionResult> CreateExcelFile([FromBody] ExcelExportModel excelExportModel)
        {
            try
            {
                var fileContentResult = await _exportExcel.ExportExcelHelper(excelExportModel);
                if (fileContentResult != null)
                {
                    return fileContentResult;
                }
                return NotFound("No data available to export.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
 
