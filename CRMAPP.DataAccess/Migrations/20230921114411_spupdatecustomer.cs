using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class spupdatecustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"CREATE PROCEDURE UpdateCustomer 
	                        @CustomerNo varchar(10), @CustomerName nvarchar(50), @CustomerSurName nvarchar(100), @Address nvarchar(150), @PostCode nvarchar(10), @Country nvarchar(100), @DateOfBirth date, @Status smallint, @UserId nvarchar(450)
                            AS
                            BEGIN
	                            SET NOCOUNT ON;
	                            BEGIN TRANSACTION; 

                                BEGIN TRY

		                            update customers
		                            set CustomerName = @CustomerName, CustomerSurName = @CustomerSurName, [Address] = @Address, PostCode = @PostCode, Country = @Country, DateOfBirth = @DateOfBirth, [Status] =@Status, UserId = @UserId
		                            Where CustomerNo = @CustomerNo
                                    COMMIT; 
                                END TRY
                                BEGIN CATCH
		                            ROLLBACK;
        
                                END CATCH;
                            END";

            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
