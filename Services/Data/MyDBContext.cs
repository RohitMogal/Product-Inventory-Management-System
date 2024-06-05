using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ServicesContract.Model;
using ServicesContracts.Model;

namespace Services.Data
{

    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductsModel> Product { get; set; }


        /// <summary>
        /// Stored procedure to add a product.
        /// </summary>
        /// <param name="product">The product model to add.</param>
        /// <returns>Returns the number of affected rows.</returns>
        
        public int sp_AddProduct(ProductsModel product)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name",product.Name),
                new SqlParameter("@Category",product.Category),
                new SqlParameter("@Price",product.Price),
                new SqlParameter("@StockQuantity",product.StockQuantity),
                new SqlParameter("@LastUpdatedDate",product.LastUpdatedDate)
            };
            int products = Database.ExecuteSqlRaw("EXEC [dbo].[AddProducts] @Name,@Category,@Price,@StockQuantity,@LastUpdatedDate", parameters);
            return products;
        }


        /// <summary>
        /// Stored procedure to get all products.
        /// </summary>
        /// <returns>Returns a list of all products.</returns>

        public List<ProductsModel> sp_GetProducts()
        {
            return Product.FromSqlRaw("Execute [dbo].[GetAllProducts]").ToList();
        }



        /// <summary>
        /// Stored procedure to get data for Excel sheet according to category.
        /// </summary>
        /// <param name="excelExportModel">The model containing export parameters.</param>
        /// <returns>Returns a list of products based on the provided category.</returns>

        public List<ProductsModel> sp_ExportExcelCategory(ExcelExportModel excelExportModel)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                  new SqlParameter("@Category", excelExportModel.Category ?? (object)DBNull.Value)
            };

            List<ProductsModel> products = Product.FromSqlRaw("EXEC [dbo].[ExportExcel2] @Category", parameters).ToList();

            return products;
        }



    }
}

