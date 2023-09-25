using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class spCustomNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"CREATE PROCEDURE spCustomerGetByCustomerNo
	            @CustomerNo nvarchar(10)
	
                AS
                BEGIN
	                SET NOCOUNT ON;

	                SELECT * from dbo.customers Where CustomerNo = @CustomerNo
                END";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
