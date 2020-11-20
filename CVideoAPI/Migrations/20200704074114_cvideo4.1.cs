using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CVideoAPI.Migrations
{
    public partial class cvideo41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerBy",
                table: "Account");

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "Video",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "Video",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "Video",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "Video",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "SectionField",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "SectionField",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "SectionField",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "SectionField",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "Section",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "Section",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "Section",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "Section",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "RecruitmentPost",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "RecruitmentPost",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "RecruitmentPost",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "RecruitmentPost",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "CV",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "CV",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "CV",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "CV",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccessKey",
                columns: table => new
                {
                    Key = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessKey", x => new { x.Key, x.UserId });
                    table.ForeignKey(
                        name: "FK_AccessKey_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Video_AccessKeyKey_AccessKeyUserId",
                table: "Video",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_SectionField_AccessKeyKey_AccessKeyUserId",
                table: "SectionField",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Section_AccessKeyKey_AccessKeyUserId",
                table: "Section",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentPost_AccessKeyKey_AccessKeyUserId",
                table: "RecruitmentPost",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_CV_AccessKeyKey_AccessKeyUserId",
                table: "CV",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessKey_UserId",
                table: "AccessKey",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CV_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "CV",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" },
                principalTable: "AccessKey",
                principalColumns: new[] { "Key", "UserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentPost_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "RecruitmentPost",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" },
                principalTable: "AccessKey",
                principalColumns: new[] { "Key", "UserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "Section",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" },
                principalTable: "AccessKey",
                principalColumns: new[] { "Key", "UserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionField_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "SectionField",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" },
                principalTable: "AccessKey",
                principalColumns: new[] { "Key", "UserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "Video",
                columns: new[] { "AccessKeyKey", "AccessKeyUserId" },
                principalTable: "AccessKey",
                principalColumns: new[] { "Key", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CV_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "CV");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentPost_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "RecruitmentPost");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionField_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "SectionField");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_AccessKey_AccessKeyKey_AccessKeyUserId",
                table: "Video");

            migrationBuilder.DropTable(
                name: "AccessKey");

            migrationBuilder.DropIndex(
                name: "IX_Video_AccessKeyKey_AccessKeyUserId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_SectionField_AccessKeyKey_AccessKeyUserId",
                table: "SectionField");

            migrationBuilder.DropIndex(
                name: "IX_Section_AccessKeyKey_AccessKeyUserId",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_RecruitmentPost_AccessKeyKey_AccessKeyUserId",
                table: "RecruitmentPost");

            migrationBuilder.DropIndex(
                name: "IX_CV_AccessKeyKey_AccessKeyUserId",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "AccessKeyId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "AccessKeyKey",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "AccessKeyUserId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "OwnerBy",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "AccessKeyId",
                table: "SectionField");

            migrationBuilder.DropColumn(
                name: "AccessKeyKey",
                table: "SectionField");

            migrationBuilder.DropColumn(
                name: "AccessKeyUserId",
                table: "SectionField");

            migrationBuilder.DropColumn(
                name: "OwnerBy",
                table: "SectionField");

            migrationBuilder.DropColumn(
                name: "AccessKeyId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "AccessKeyKey",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "AccessKeyUserId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "OwnerBy",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "AccessKeyId",
                table: "RecruitmentPost");

            migrationBuilder.DropColumn(
                name: "AccessKeyKey",
                table: "RecruitmentPost");

            migrationBuilder.DropColumn(
                name: "AccessKeyUserId",
                table: "RecruitmentPost");

            migrationBuilder.DropColumn(
                name: "OwnerBy",
                table: "RecruitmentPost");

            migrationBuilder.DropColumn(
                name: "AccessKeyId",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "AccessKeyKey",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "AccessKeyUserId",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "OwnerBy",
                table: "CV");

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
