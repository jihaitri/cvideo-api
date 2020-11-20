using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CVideoAPI.Migrations
{
    public partial class updatesecurity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "SectionField");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "Section");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "Video",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "SectionField",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "Section",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
