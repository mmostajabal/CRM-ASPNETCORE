using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class spLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"Create PROCEDURE [dbo].[spGetStatement]
	
	                        @language char(2), @Statmentkey nvarchar(100)
                            AS
                        BEGIN
	                        SET NOCOUNT ON;

                            -- Insert statements for procedure here
	                        Select [StatementInLang] from dbo.Languages where [LanguageCode] = @language and Statmentkey = @Statmentkey
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
