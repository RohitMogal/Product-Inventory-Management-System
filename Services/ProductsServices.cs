using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Data;
using ServicesContracts;
using ServicesContracts.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class ProductsServices : IProducts
    {
        private readonly MyDBContext _myDBContext;
        private readonly ILogger<ProductsServices> _logger;

        public ProductsServices(MyDBContext myDBContext, ILogger<ProductsServices> logger)
        {
            _myDBContext = myDBContext;
            _logger = logger;
        }

        public async Task<bool> AddProductsAsync(ProductsModel products)
        {
            try
            {
                await _myDBContext.AddProduct1Async(products);
                await _myDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating product: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _myDBContext.Product.FindAsync(productId);
                if (product == null)
                {
                    return false;
                }
                else
                {
                    _myDBContext.Product.Remove(product);
                    await _myDBContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting product: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ProductsModel>> GetProductsAsync()
        {
            try
            {
                return await _myDBContext.GetAllProducts1Async();
             
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving products: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductsModel updatedproducts)
        {
            try
            {
                var product = await _myDBContext.Product.FindAsync(updatedproducts.ProductID);
                if (product != null)
                {
                    product.Price = updatedproducts.Price;
                    product.StockQuantity = updatedproducts.StockQuantity;
                    product.Name = updatedproducts.Name;
                    product.Category = updatedproducts.Category;
                    product.LastUpdatedDate = updatedproducts.LastUpdatedDate;

                    _myDBContext.Product.Update(product);
                    await _myDBContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating product: {ex.Message}");
                throw;
            }
        }
    }
}
