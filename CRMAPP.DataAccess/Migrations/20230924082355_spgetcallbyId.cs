using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class spgetcallbyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"CREATE PROCEDURE GetCallById

	                        @Id Int
                        AS
                        BEGIN
	                        SET NOCOUNT ON;


	                        Select * from Call Where Id = @Id
                        END";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
