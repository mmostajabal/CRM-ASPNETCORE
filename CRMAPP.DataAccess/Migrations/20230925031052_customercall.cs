using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class customercall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"Create PROCEDURE [dbo].[spCustomerCall] 
	                     AS
                        BEGIN
	                        SET NOCOUNT ON;

	                        SELECT dbo.Customers.CustomerNo, dbo.Customers.CustomerName, dbo.Customers.CustomerSurName, dbo.Customers.Address, dbo.Customers.PostCode, dbo.Customers.Country, dbo.Call.Subject, dbo.Call.Description, dbo.Call.Status, CASE WHEN call.[Status] = 1 THEN 'Active' WHEN [Call].[Status] = 2 THEN 'Deleted' ELSE '-' END AS StatusDesc
	                        FROM dbo.Call INNER JOIN
                            dbo.Customers ON dbo.Call.CustomerNo = dbo.Customers.CustomerNo
                        END
                        ";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
