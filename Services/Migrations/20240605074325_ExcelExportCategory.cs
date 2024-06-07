using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class ExcelExportCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_ExportExcelCategory = @"
        CREATE PROCEDURE [dbo].[ExportExcel2]
            @Category nvarchar(100)
        AS
        BEGIN
            SELECT  
                [ProductID],
                [Name],
                [Category],
                [Price],
                [StockQuantity],
                [LastUpdatedDate]
            FROM 
                [ProductDB].[dbo].[Product]
            WHERE 
                (Category = @Category OR @Category IS NULL)
        END
    ";

            migrationBuilder.Sql(sp_ExportExcelCategory);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_ExportExcelCategory = @"
               DROP PROCEDURE [dbo].[ExportExcel2]";

            migrationBuilder.Sql(sp_ExportExcelCategory);
        }
    }
}
