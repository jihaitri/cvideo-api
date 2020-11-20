using Microsoft.EntityFrameworkCore.Migrations;

namespace CVideoAPI.Migrations
{
    public partial class renamepkapplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ApplyId",
                table: "Application");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Application",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Application",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Application");

            migrationBuilder.AddColumn<int>(
                name: "ApplyId",
                table: "Application",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Application",
                column: "ApplyId");
        }
    }
}
