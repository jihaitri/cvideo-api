using Microsoft.EntityFrameworkCore.Migrations;

namespace CVideoAPI.Migrations
{
    public partial class updateVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Video");

            migrationBuilder.AddColumn<double>(
                name: "AspectRatio",
                table: "Video",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Video",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbUrl",
                table: "Video",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Video",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspectRatio",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "ThumbUrl",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Video");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Video",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
