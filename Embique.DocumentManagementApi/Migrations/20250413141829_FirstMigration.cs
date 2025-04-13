using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Embique.DocumentManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "documentModels",
                columns: table => new
                {
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileType = table.Column<int>(type: "integer", nullable: false),
                    FileUploadedBy = table.Column<string>(type: "text", nullable: false),
                    Metadata = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentModels", x => x.FileId);
                });

            migrationBuilder.InsertData(
                table: "documentModels",
                columns: new[] { "FileId", "Author", "DateOfCreation", "FilePath", "FileType", "FileUploadedBy", "Metadata", "Title" },
                values: new object[,]
                {
                    { new Guid("5bfe2e5e-d8b7-4786-b2e9-22fd8fdcc022"), "DEF", new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "/uploads/sample2.docx", 1, "User2", "Data1", "Sample Document 2" },
                    { new Guid("9c1f5f7b-bf53-4f75-85e3-d9a1d5e4e2a1"), "ABC", new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "/uploads/sample1.pdf", 0, "User1", "Data1", "Sample Document 1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documentModels");
        }
    }
}
