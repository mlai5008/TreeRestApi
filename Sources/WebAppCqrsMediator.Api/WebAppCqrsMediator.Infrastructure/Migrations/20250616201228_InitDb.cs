using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppCqrsMediator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecureExceptionModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecureExceptionModels", x => x.Id);
                });

            var spCreateInsertExceptionScript = @"CREATE PROCEDURE SpInsertSecureExceptionModel
    @Name NVARCHAR(100),
    @Message NVARCHAR(max),
	@Type NVARCHAR(100),
	@Source NVARCHAR(max),
	@Url NVARCHAR(100),
    @Id uniqueidentifier OUTPUT
AS
BEGIN
    INSERT INTO SecureExceptionModels (Id, Name, Datetime, Message, Type, Source, Url)
    VALUES (NEWID(), @Name, GETDATE(), @Message, @Type, @Source, @Url);    
END";
            migrationBuilder.Sql(spCreateInsertExceptionScript);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "SecureExceptionModels");
        }
    }
}
