using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class GetProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string GetAllProducts1 = @"
                CREATE PROCEDURE [dbo].[GetAllProducts1]
                AS
                BEGIN
                    BEGIN TRANSACTION;

                    BEGIN TRY
                        -- Select all products
                        SELECT * FROM dbo.Product;

                        -- Commit the transaction
                        COMMIT;
                    END TRY
                    BEGIN CATCH
                        -- Rollback the transaction on error
                        IF @@TRANCOUNT > 0
                        BEGIN
                            ROLLBACK;
                        END

                        -- Return error information
                        SELECT 
                            ERROR_LINE() AS ErrorLine, 
                            ERROR_MESSAGE() AS ErrorMessage;

                        -- Re-throw the error
                        THROW;
                    END CATCH
                END;

                ";

            migrationBuilder.Sql(GetAllProducts1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string GetAllProducts1 = @"
                DROP PROCEDURE [dbo].[GetAllProducts1]
                ";

            migrationBuilder.Sql(GetAllProducts1);
        }
    }
}
