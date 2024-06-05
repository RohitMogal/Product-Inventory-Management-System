using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.Helper;
using ServicesContracts;
using ServicesContracts.Model;

namespace Services
{
    public class ProductsServices : IProducts
    {
        private readonly MyDBContext _myDBContext;
        public ProductsServices(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }


        /// <summary>
        /// Service to add a product.
        /// </summary>
        /// <param name="products">The product model to add.</param>
        /// <returns>Returns true if the product is added successfully.</returns>
        
        public async Task<bool> AddProducts(ProductsModel products)
        {
            try
            {

                var res = _myDBContext.sp_AddProduct(products);
                var res1 = await _myDBContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) { throw ex; };
        }


        /// <summary>
        /// Service to delete a product.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <returns>Returns true if the product is deleted successfully; otherwise, false.</returns>
       
        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _myDBContext.Product.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            else
            {
                _myDBContext.Product.Remove(product);
                return true;
            }
        }


        /// <summary>
        /// Service to get all products.
        /// </summary>
        /// <returns>Returns a list of all products.</returns>
        
        public async Task<List<ProductsModel>> GetProducts()
        {
            List<ProductsModel> result = _myDBContext.sp_GetProducts();
            return result;
        }


        /// <summary>
        /// Service to update a product.
        /// </summary>
        /// <param name="updatedproducts">The updated product model.</param>
        /// <returns>Returns true if the product is updated successfully; otherwise, false.</returns>
        
        public async Task<bool> UpdateProduct(ProductsModel updatedproducts)
        {
            var product = await _myDBContext.Product.FindAsync(updatedproducts.ProductID);
            if (product != null)
            {

                product.Price = updatedproducts.Price;
                product.StockQuantity = updatedproducts.StockQuantity;
                product.Name = updatedproducts.Name;
                product.Category = updatedproducts.Category;

                _myDBContext.Product.Update(product);
                await _myDBContext.SaveChangesAsync();
                return true;
            }

            return false;
        }


    }
}
