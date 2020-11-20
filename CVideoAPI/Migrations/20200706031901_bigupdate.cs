using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CVideoAPI.Migrations
{
    public partial class bigupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Style",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Style",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Question",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Question",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QuestionTime",
                table: "Question",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Style");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Style");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "QuestionTime",
                table: "Question");
        }
    }
}
