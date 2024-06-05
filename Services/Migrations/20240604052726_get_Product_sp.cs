using Microsoft.EntityFrameworkCore.Migrations;
using System.Runtime.InteropServices;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class get_Product_sp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllProducts = @"
                CREATE PROCEDURE [dbo].[GetAllProducts]
                AS BEGIN
                SELECT * FROM dbo.Product
                END
                ";

            migrationBuilder.Sql(sp_GetAllProducts);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllProducts = @"
                DROP PROCEDURE [dbo].[GetAllProducts]
                ";

            migrationBuilder.Sql(sp_GetAllProducts);
        }
    }
}
