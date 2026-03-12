using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhoaCNTT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetupDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.CheckConstraint("CK_Admin_Level", "[Level] >= 1");
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "DATE", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectCode = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false, defaultValue: 3)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectCode);
                    table.CheckConstraint("CK_Subject_Credits", "[Credits] > 0");
                });

            migrationBuilder.CreateTable(
                name: "FileResource",
                columns: table => new
                {
                    FileResourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FilePath = table.Column<string>(type: "char(255)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileResource", x => x.FileResourceID);
                    table.ForeignKey(
                        name: "FK_FileResource_Admins_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsResource",
                columns: table => new
                {
                    NewsResourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsResource", x => x.NewsResourceID);
                    table.ForeignKey(
                        name: "FK_NewsResource_Admins_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LecturerSubjects",
                columns: table => new
                {
                    LecturerId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectCode = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerSubjects", x => new { x.LecturerId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_LecturerSubjects_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerSubjects_Subjects_SubjectCode",
                        column: x => x.SubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileEntity",
                columns: table => new
                {
                    FileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    DownloadCount = table.Column<int>(type: "int", nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectCode = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    CurrentResourceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntity", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_FileEntity_Admins_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileEntity_FileResource_CurrentResourceId",
                        column: x => x.CurrentResourceId,
                        principalTable: "FileResource",
                        principalColumn: "FileResourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileEntity_Subjects_SubjectCode",
                        column: x => x.SubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode");
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    CurrentResourceId = table.Column<int>(type: "int", nullable: false),
                    NewsType = table.Column<string>(type: "varchar(50)", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsID);
                    table.ForeignKey(
                        name: "FK_News_Admins_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_News_NewsResource_CurrentResourceId",
                        column: x => x.CurrentResourceId,
                        principalTable: "NewsResource",
                        principalColumn: "NewsResourceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileRequest",
                columns: table => new
                {
                    FileRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SubjectCode = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetFileId = table.Column<int>(type: "int", nullable: true),
                    NewResourceId = table.Column<int>(type: "int", nullable: false),
                    OldResourceId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileRequest", x => x.FileRequestID);
                    table.ForeignKey(
                        name: "FK_FileRequest_FileEntity_TargetFileId",
                        column: x => x.TargetFileId,
                        principalTable: "FileEntity",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FileRequest_FileResource_NewResourceId",
                        column: x => x.NewResourceId,
                        principalTable: "FileResource",
                        principalColumn: "FileResourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileRequest_FileResource_OldResourceId",
                        column: x => x.OldResourceId,
                        principalTable: "FileResource",
                        principalColumn: "FileResourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileRequest_Subjects_SubjectCode",
                        column: x => x.SubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MSV = table.Column<string>(type: "CHAR(10)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsRequest",
                columns: table => new
                {
                    NewsRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestType = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    NewsType = table.Column<string>(type: "varchar(50)", nullable: false),
                    TargetNewsId = table.Column<int>(type: "int", nullable: true),
                    NewResourceId = table.Column<int>(type: "int", nullable: false),
                    OldResourceId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsRequest", x => x.NewsRequestID);
                    table.ForeignKey(
                        name: "FK_NewsRequest_NewsResource_NewResourceId",
                        column: x => x.NewResourceId,
                        principalTable: "NewsResource",
                        principalColumn: "NewsResourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsRequest_NewsResource_OldResourceId",
                        column: x => x.OldResourceId,
                        principalTable: "NewsResource",
                        principalColumn: "NewsResourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsRequest_News_TargetNewsId",
                        column: x => x.TargetNewsId,
                        principalTable: "News",
                        principalColumn: "NewsID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FileApproval",
                columns: table => new
                {
                    FileApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileRequestId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: false),
                    Decision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileApproval", x => x.FileApprovalID);
                    table.ForeignKey(
                        name: "FK_FileApproval_Admins_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileApproval_FileRequest_FileRequestId",
                        column: x => x.FileRequestId,
                        principalTable: "FileRequest",
                        principalColumn: "FileRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsApproval",
                columns: table => new
                {
                    NewsApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsRequestId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: false),
                    Decision = table.Column<string>(type: "varchar(50)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsApproval", x => x.NewsApprovalID);
                    table.ForeignKey(
                        name: "FK_NewsApproval_Admins_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsApproval_NewsRequest_NewsRequestId",
                        column: x => x.NewsRequestId,
                        principalTable: "NewsRequest",
                        principalColumn: "NewsRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Username",
                table: "Admins",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_NewsId",
                table: "Comment",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_FileApproval_ApproverId",
                table: "FileApproval",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_FileApproval_FileRequestId",
                table: "FileApproval",
                column: "FileRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_FileEntity_CreatedBy",
                table: "FileEntity",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FileEntity_CurrentResourceId",
                table: "FileEntity",
                column: "CurrentResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FileEntity_SubjectCode",
                table: "FileEntity",
                column: "SubjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_FileRequest_NewResourceId",
                table: "FileRequest",
                column: "NewResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FileRequest_OldResourceId",
                table: "FileRequest",
                column: "OldResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FileRequest_SubjectCode",
                table: "FileRequest",
                column: "SubjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_FileRequest_TargetFileId",
                table: "FileRequest",
                column: "TargetFileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileResource_CreatedBy",
                table: "FileResource",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_Email",
                table: "Lecturers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LecturerSubjects_SubjectCode",
                table: "LecturerSubjects",
                column: "SubjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_News_CreatedById",
                table: "News",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_News_CurrentResourceId",
                table: "News",
                column: "CurrentResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsApproval_ApproverId",
                table: "NewsApproval",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsApproval_NewsRequestId",
                table: "NewsApproval",
                column: "NewsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsRequest_NewResourceId",
                table: "NewsRequest",
                column: "NewResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsRequest_OldResourceId",
                table: "NewsRequest",
                column: "OldResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsRequest_TargetNewsId",
                table: "NewsRequest",
                column: "TargetNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsResource_CreatedBy",
                table: "NewsResource",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectCode",
                table: "Subjects",
                column: "SubjectCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "FileApproval");

            migrationBuilder.DropTable(
                name: "LecturerSubjects");

            migrationBuilder.DropTable(
                name: "NewsApproval");

            migrationBuilder.DropTable(
                name: "FileRequest");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "NewsRequest");

            migrationBuilder.DropTable(
                name: "FileEntity");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "FileResource");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "NewsResource");

            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
