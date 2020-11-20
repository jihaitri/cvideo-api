using Microsoft.EntityFrameworkCore.Migrations;

namespace CVideoAPI.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_RecruitmentPost_RecruitmentPostPostId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_RecruitmentPostPostId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "RecruitmentPostPostId",
                table: "Application");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_RecruitmentPost_PostId",
                table: "Application",
                column: "PostId",
                principalTable: "RecruitmentPost",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_RecruitmentPost_PostId",
                table: "Application");

            migrationBuilder.AddColumn<int>(
                name: "RecruitmentPostPostId",
                table: "Application",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_RecruitmentPostPostId",
                table: "Application",
                column: "RecruitmentPostPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_RecruitmentPost_RecruitmentPostPostId",
                table: "Application",
                column: "RecruitmentPostPostId",
                principalTable: "RecruitmentPost",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
