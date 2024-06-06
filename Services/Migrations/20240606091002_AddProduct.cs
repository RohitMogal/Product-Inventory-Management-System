using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string AddProduct1 = @"
               CREATE PROCEDURE [dbo].[AddProducts1]
(
    @Name nvarchar(100),
    @Category nvarchar(100),
    @Price float,
    @StockQuantity int,
    @LastUpdatedDate datetime2(7)
)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insert statement
        INSERT INTO PRODUCT (Name, Category, Price, StockQuantity, LastUpdatedDate)
        VALUES (@Name, @Category, @Price, @StockQuantity, @LastUpdatedDate);

        -- Commit transaction
        COMMIT;
    END TRY
    BEGIN CATCH
        -- Rollback transaction on error
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK;
        END

        -- Return error information
        SELECT ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage;

        -- Re-throw the error
        THROW;
    END CATCH
END;

                ";

            migrationBuilder.Sql(AddProduct1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string AddProduct1 = @"
                Drop PROCEDURE [dbo].[AddProducts1]
                ";

            migrationBuilder.Sql(AddProduct1);
        }
    }
}
