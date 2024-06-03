using ServicesContracts.Model;

namespace ServicesContracts
{
    public interface IProducts
    {
        public  Task<List<ProductsModel>> GetProducts();
        public Task<int> AddProducts(ProductsModel products);

        public Task<bool> UpdateProduct(ProductsModel products);

        public Task<bool> DeleteProduct(int productId);

    }
}
