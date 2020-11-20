using Microsoft.EntityFrameworkCore.Migrations;

namespace CVideoAPI.Migrations
{
    public partial class updatemajori18n : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MajorName",
                table: "Major");

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Translation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translation_MajorId",
                table: "Translation",
                column: "MajorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Major_MajorId",
                table: "Translation",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Major_MajorId",
                table: "Translation");

            migrationBuilder.DropIndex(
                name: "IX_Translation_MajorId",
                table: "Translation");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Translation");

            migrationBuilder.AddColumn<string>(
                name: "MajorName",
                table: "Major",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
