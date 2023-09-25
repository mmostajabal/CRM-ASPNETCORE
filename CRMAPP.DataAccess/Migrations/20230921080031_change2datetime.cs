using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class change2datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Customers",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Call",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            string sql = @"Create PROCEDURE spInsert2Customer
	                        @CustomerName nvarchar(50), @CustomerSurName nvarchar(100), @Address nvarchar(150), @PostCode nvarchar(10), @Country nvarchar(100), @DateOfBirth date, @Status smallint, @UserId nvarchar(450)
	
                        AS
                        BEGIN
	                        SET NOCOUNT ON;

                            BEGIN TRANSACTION; 

                            BEGIN TRY
		                        DECLARE @CurrentYear INT;
		                        DECLARE @SerialNo varchar(6); 
		                        DECLARE @CustomerNO varchar(10)

		                        SET @CurrentYear = YEAR(GETDATE());

		                        Select @SerialNo = Cast(SerialNo  as varchar(6)) from dbo.customerNos where CurYear = @CurrentYear;
		
		                        IF @SerialNo IS NOT NULL
		                        BEGIN
			                        Update dbo.customerNos Set  SerialNo = SerialNo + 1  where CurYear = @CurrentYear;
		                        END
		                        ELSE
		                        BEGIN
			                        Set @SerialNo = 1;
			                        Insert Into dbo.customerNos(CurYear, SerialNo) Values(@CurrentYear, @SerialNo+ 1);
		                        END;
		
		                        SELECT  @CustomerNO =( cast(@CurrentYear as varchar) + Cast(Right(REPLICATE('0',6-LEN(@SerialNo))+@SerialNo,6)  as varchar)) ;
		                        select @CustomerNO

                                INSERT INTO Customers (CustomerNo,CustomerName, CustomerSurName, Address, PostCode, Country, DateOfBirth, [Status], UserId, LastUpdate)
                                VALUES (@CustomerNo, @CustomerName, @CustomerSurName, @Address, @PostCode, @Country, @DateOfBirth, @Status, @UserId, GETDATE());

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
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Customers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Call",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
