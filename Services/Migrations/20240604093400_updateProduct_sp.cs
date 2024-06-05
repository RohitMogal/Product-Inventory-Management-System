using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class updateProduct_sp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_UpdateProduct = @"
                CREATE PROCEDURE [dbo].[UpdateProducts]
                (@Name nvarchar(100),@Category nvarchar(100),@Price float,@StockQuantity int,@ProductID int)
                AS BEGIN
                UPDATE PRODUCT SET Name=@Name,Category=@Category,Price=@Price,StockQuantity=@StockQuantity WHERE ProductID=@ProductID
                END
                ";

            migrationBuilder.Sql(sp_UpdateProduct);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_UpdateProduct = @"
                Drop PROCEDURE [dbo].[UpdateProducts]
                ";

            migrationBuilder.Sql(sp_UpdateProduct);
        }
    }
}
