using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class spgetALLCalls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"CREATE PROCEDURE spGetCalls4Customer
	
	                        @CustomerNo varchar(10)
                        AS
                        BEGIN
	                        SET NOCOUNT ON;


	                        Select * from Call Where CustomerNo = @CustomerNo
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
