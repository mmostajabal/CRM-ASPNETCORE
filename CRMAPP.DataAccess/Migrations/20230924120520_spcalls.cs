using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMAPP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class spcalls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"Create PROCEDURE [dbo].[spInsert2Call] 
	                        @CustomerNo varchar(10), @CallDate date, @CallTime time, @Subject nvarchar(150), @Description nvarchar(max), @Status smallint, @UserId nvarchar(450)
                        AS
                        BEGIN
	                        SET NOCOUNT ON;

	                        INSERT INTO [dbo].[Call]([CustomerNo], [CallDate] ,[CallTime] ,[Subject] ,[Description] ,[LastUpdate] ,[Status] ,[UserId])
                             VALUES
                                   (@CustomerNo, @CallDate, @CallTime, @Subject, @Description, GETDATE(), @Status, @UserId)
                        END";

            migrationBuilder.Sql(sql);

            sql = @"CREATE PROCEDURE spUpdateCall
	                @Id int, @CallDate date, @CallTime time, @Subject nvarchar(150), @Description nvarchar(max), @Status smallint, @UserId nvarchar(450)
                AS
                BEGIN
	                SET NOCOUNT ON;
	                UPDATE [dbo].[Call]
                   SET [CallDate] = @CallDate
                      ,[CallTime] = @CallTime
                      ,[Subject] = @Subject
                      ,[Description] = @Description
                      ,[LastUpdate] = GETDATE()
                      ,[Status] = @Status
                      ,[UserId] = @UserId
                 WHERE Id = @Id
                END";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
