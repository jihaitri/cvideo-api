using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CVideoAPI.Migrations
{
    public partial class cvideo43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessKey",
                table: "AccessKey");

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

            migrationBuilder.DropColumn(
                name: "Key",
                table: "AccessKey");

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "Video",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "SectionField",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "Section",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "RecruitmentPost",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "Employee",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "CV",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "Company",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataKey",
                table: "AccessKey",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessKey",
                table: "AccessKey",
                columns: new[] { "DataKey", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessKey",
                table: "AccessKey");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "SectionField");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "RecruitmentPost");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "CV");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "DataKey",
                table: "AccessKey");

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "Video",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "Video",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "Video",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "Video",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "SectionField",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "SectionField",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "SectionField",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "SectionField",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "Section",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "Section",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "Section",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "Section",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "RecruitmentPost",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "RecruitmentPost",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "RecruitmentPost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "RecruitmentPost",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyId",
                table: "CV",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessKeyKey",
                table: "CV",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessKeyUserId",
                table: "CV",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerBy",
                table: "CV",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                table: "AccessKey",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessKey",
                table: "AccessKey",
                columns: new[] { "Key", "UserId" });

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
    }
}
