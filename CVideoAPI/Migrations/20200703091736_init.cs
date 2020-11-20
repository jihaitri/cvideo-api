using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CVideoAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    MajorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major", x => x.MajorId);
                });

            migrationBuilder.CreateTable(
                name: "NewsFeedSection",
                columns: table => new
                {
                    NewsFeedSectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    DispOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeedSection", x => x.NewsFeedSectionId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SectionType",
                columns: table => new
                {
                    SectionTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    DispOrder = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionType", x => x.SectionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Style",
                columns: table => new
                {
                    StyleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StyleName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Style", x => x.StyleId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Account_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSet",
                columns: table => new
                {
                    SetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    SetName = table.Column<string>(nullable: false),
                    SectionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSet", x => x.SetId);
                    table.ForeignKey(
                        name: "FK_QuestionSet_SectionType_SectionTypeId",
                        column: x => x.SectionTypeId,
                        principalTable: "SectionType",
                        principalColumn: "SectionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    NewsFeedSectionId = table.Column<int>(nullable: true),
                    SectionTypeId = table.Column<int>(nullable: true),
                    Language = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translation_NewsFeedSection_NewsFeedSectionId",
                        column: x => x.NewsFeedSectionId,
                        principalTable: "NewsFeedSection",
                        principalColumn: "NewsFeedSectionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Translation_SectionType_SectionTypeId",
                        column: x => x.SectionTypeId,
                        principalTable: "SectionType",
                        principalColumn: "SectionTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 500, nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Company_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Employee_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<int>(nullable: true),
                    DeviceId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDevice_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionContent = table.Column<string>(nullable: false),
                    SetId = table.Column<int>(nullable: false),
                    DispOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_QuestionSet_SetId",
                        column: x => x.SetId,
                        principalTable: "QuestionSet",
                        principalColumn: "SetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentPost",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    ExpectedNumber = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    JobDescription = table.Column<string>(nullable: true),
                    JobRequirement = table.Column<string>(nullable: true),
                    JobBenefit = table.Column<string>(nullable: true),
                    MajorId = table.Column<int>(nullable: false),
                    MinSalary = table.Column<long>(nullable: false),
                    MaxSalary = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentPost", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_RecruitmentPost_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecruitmentPost_Major_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "MajorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    CVId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    MajorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV", x => x.CVId);
                    table.ForeignKey(
                        name: "FK_CV_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CV_Major_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "MajorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    ApplyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    RecruitmentPostPostId = table.Column<int>(nullable: true),
                    CVId = table.Column<int>(nullable: false),
                    Viewed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.ApplyId);
                    table.UniqueConstraint("AK_Application_PostId_CVId", x => new { x.PostId, x.CVId });
                    table.ForeignKey(
                        name: "FK_Application_CV_CVId",
                        column: x => x.CVId,
                        principalTable: "CV",
                        principalColumn: "CVId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_RecruitmentPost_RecruitmentPostPostId",
                        column: x => x.RecruitmentPostPostId,
                        principalTable: "RecruitmentPost",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    SectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    DisplayTitle = table.Column<string>(nullable: false),
                    SectionTypeId = table.Column<int>(nullable: false),
                    CVId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.SectionId);
                    table.ForeignKey(
                        name: "FK_Section_CV_CVId",
                        column: x => x.CVId,
                        principalTable: "CV",
                        principalColumn: "CVId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Section_SectionType_SectionTypeId",
                        column: x => x.SectionTypeId,
                        principalTable: "SectionType",
                        principalColumn: "SectionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionField",
                columns: table => new
                {
                    FieldId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    FieldTitle = table.Column<string>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionField", x => x.FieldId);
                    table.ForeignKey(
                        name: "FK_SectionField_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    VideoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Path = table.Column<string>(nullable: false),
                    StyleId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_Video_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Video_Style_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Style",
                        principalColumn: "StyleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 2, "employee" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 3, "employer" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleId",
                table: "Account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_CVId",
                table: "Application",
                column: "CVId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_RecruitmentPostPostId",
                table: "Application",
                column: "RecruitmentPostPostId");

            migrationBuilder.CreateIndex(
                name: "IX_CV_EmployeeId",
                table: "CV",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CV_MajorId",
                table: "CV",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_CV_Title_EmployeeId",
                table: "CV",
                columns: new[] { "Title", "EmployeeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_SetId",
                table: "Question",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSet_SectionTypeId",
                table: "QuestionSet",
                column: "SectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentPost_CompanyId",
                table: "RecruitmentPost",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentPost_MajorId",
                table: "RecruitmentPost",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_CVId",
                table: "Section",
                column: "CVId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_SectionTypeId",
                table: "Section",
                column: "SectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionField_SectionId",
                table: "SectionField",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_NewsFeedSectionId",
                table: "Translation",
                column: "NewsFeedSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_SectionTypeId",
                table: "Translation",
                column: "SectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_Language_NewsFeedSectionId",
                table: "Translation",
                columns: new[] { "Language", "NewsFeedSectionId" },
                unique: true,
                filter: "[NewsFeedSectionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_Language_SectionTypeId",
                table: "Translation",
                columns: new[] { "Language", "SectionTypeId" },
                unique: true,
                filter: "[SectionTypeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevice_AccountId",
                table: "UserDevice",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_SectionId",
                table: "Video",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_StyleId",
                table: "Video",
                column: "StyleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "SectionField");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "UserDevice");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "RecruitmentPost");

            migrationBuilder.DropTable(
                name: "QuestionSet");

            migrationBuilder.DropTable(
                name: "NewsFeedSection");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Style");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "SectionType");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Major");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
