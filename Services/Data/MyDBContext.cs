using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServicesContract.Model;
using ServicesContracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Data
{
    public class MyDBContext : DbContext
    {
        private readonly ILogger<MyDBContext> _logger;

        public MyDBContext(DbContextOptions options, ILogger<MyDBContext> logger) : base(options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public DbSet<ProductsModel> Product { get; set; }

        /// <summary>
        /// Adds a product using a stored procedure.
        /// </summary>
        /// <param name="product">The product model to add.</param>
        /// <returns>Returns the number of affected rows.</returns>
        public async Task<int> AddProduct1Async(ProductsModel product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", product.Name),
                new SqlParameter("@Category", product.Category),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@StockQuantity", product.StockQuantity),
                new SqlParameter("@LastUpdatedDate", product.LastUpdatedDate)
            };

            try
            {
                _logger.LogInformation("Executing stored procedure [dbo].[AddProducts]");
                return await Database.ExecuteSqlRawAsync("EXEC [dbo].[AddProducts] @Name,@Category,@Price,@StockQuantity,@LastUpdatedDate", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding product.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves all products using a stored procedure.
        /// </summary>
        /// <returns>Returns a list of all products.</returns>
        public async Task<List<ProductsModel>> GetAllProducts1Async()
        {
            try
            {
                _logger.LogInformation("Executing stored procedure [dbo].[GetAllProducts]");
                return await Product.FromSqlRaw("EXEC [dbo].[GetAllProducts]").ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves products by category for Excel export using a stored procedure.
        /// </summary>
        /// <param name="excelExportModel">The model containing export parameters.</param>
        /// <returns>Returns a list of products based on the provided category.</returns>
        public async Task<List<ProductsModel>> ExportExcelCategoryAsync(ExcelExportModel excelExportModel)
        {
            if (excelExportModel == null)
                throw new ArgumentNullException(nameof(excelExportModel));

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Category", excelExportModel.Category ?? (object)DBNull.Value)
            };

            try
            {
                _logger.LogInformation("Executing stored procedure [dbo].[ExportExcel2] with category {Category}", excelExportModel.Category);
                return await Product.FromSqlRaw("EXEC [dbo].[ExportExcel2] @Category", parameters).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while exporting products for Excel.");
                throw;
            }
        }
    }
}