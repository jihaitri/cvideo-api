using Microsoft.EntityFrameworkCore.Migrations;

namespace CVideoAPI.Migrations
{
    public partial class updatecvmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ExpectedNumber",
                table: "RecruitmentPost",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ExpectedNumber",
                table: "RecruitmentPost",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
