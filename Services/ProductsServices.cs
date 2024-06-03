using Microsoft.EntityFrameworkCore;
using Services.Data;
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

        public async Task<int> AddProducts(ProductsModel products)
        {
            var product = new ProductsModel()
            {
                Name=products.Name,
                Category = products.Category,
                Price = products.Price,
                StockQuantity = products.StockQuantity,

            };
            _myDBContext.Product.Add(product);
            var result=await _myDBContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _myDBContext.Product.FindAsync(productId);
            if(product==null)
            { 
                return false;
            }
            else
            {
                var result = _myDBContext.Product.Remove(product);
                return true;
            }
           

        }

        public async Task<List<ProductsModel>> GetProducts()
        {
            var result = await _myDBContext.Product.ToListAsync();
            return result;
        }

        public async Task<bool> UpdateProduct(ProductsModel updatedproducts)
        {
            var product = await _myDBContext.Product.FindAsync(updatedproducts.ProductID);
            if(product!=null) {

                product.Price= updatedproducts.Price;
                product.StockQuantity= updatedproducts.StockQuantity;
                product.Name = updatedproducts.Name;
                product.Category= updatedproducts.Category;

                 _myDBContext.Product.Update(product);
                await _myDBContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

      
    }
}
