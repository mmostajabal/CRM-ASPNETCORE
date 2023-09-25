using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class storeprocCustomerGetAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"Create PROCEDURE [dbo].[spCustomerGetAll]
            AS
            BEGIN
	            SET NOCOUNT ON;

                -- Insert statements for procedure here
	            SELECT * from dbo.customers
            END";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = @"DROP PROCEDURE [dbo].[spCustomerGetAll]";
            migrationBuilder.Sql(sql);
        }
    }
}
