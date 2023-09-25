using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class language1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanuageCode",
                table: "languages",
                newName: "LanguageCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanguageCode",
                table: "languages",
                newName: "LanuageCode");
        }
    }
}
