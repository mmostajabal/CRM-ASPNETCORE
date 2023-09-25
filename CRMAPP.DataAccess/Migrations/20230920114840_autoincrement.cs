using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class autoincrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Call_customers_CustomerNo",
                table: "Call");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_customers_CustomerNo",
                table: "Customers",
                newName: "IX_Customers_CustomerNo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Call_Customers_CustomerNo",
                table: "Call",
                column: "CustomerNo",
                principalTable: "Customers",
                principalColumn: "CustomerNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Call_Customers_CustomerNo",
                table: "Call");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CustomerNo",
                table: "customers",
                newName: "IX_customers_CustomerNo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "CustomerNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Call_customers_CustomerNo",
                table: "Call",
                column: "CustomerNo",
                principalTable: "customers",
                principalColumn: "CustomerNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
