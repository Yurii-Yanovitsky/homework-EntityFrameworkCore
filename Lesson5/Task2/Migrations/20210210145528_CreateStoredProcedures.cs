using Microsoft.EntityFrameworkCore.Migrations;

namespace Task2.Migrations
{
    public partial class CreateStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[GetPhoneByID_1]
@paramId int,
@Id int OUT,
@Name varchar(25) OUT,
@Price int OUT
AS
SELECT @ID = p.Id, @Name = p.[Name], @Price = p.Price FROM Phones as p
WHERE p.Id = @paramId
GO");
            migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[GetPhoneByID_2]
@paramId int
AS
SELECT * FROM Phones as p
WHERE p.Id = @paramId
GO");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetPhoneByID_1]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetPhoneByID_2]");
        }
    }
}
