using ServicesContracts.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesContracts
{
    public interface IProducts
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public Task<List<ProductsModel>> GetProductsAsync();



        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="products">The product model to add.</param>
        /// <returns>True if the product is added successfully; otherwise, false.</returns>
        public Task<bool> AddProductsAsync(ProductsModel products);



        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="products">The updated product model.</param>
        /// <returns>True if the product is updated successfully; otherwise, false.</returns>
        public Task<bool> UpdateProductAsync(ProductsModel products);



        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <returns>True if the product is deleted successfully; otherwise, false.</returns>
        public Task<bool> DeleteProductAsync(int productId);
    }
}
