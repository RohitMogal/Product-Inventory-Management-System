using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class sp_addPorduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_AddProduct = @"
                CREATE PROCEDURE [dbo].[AddProducts]
                (@Name nvarchar(100),@Category nvarchar(100),@Price float,@StockQuantity int,@LastUpdatedDate datetime2(7))
                AS BEGIN
                INSERT INTO PRODUCT(Name,Category,Price,StockQuantity,LastUpdatedDate) Values(@Name, @Category, @Price, @StockQuantity, @LastUpdatedDate)
                END
                ";

            migrationBuilder.Sql(sp_AddProduct);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_AddProduct = @"
                Drop PROCEDURE [dbo].[AddProducts]
                ";

            migrationBuilder.Sql(sp_AddProduct);
        }
    }
}
