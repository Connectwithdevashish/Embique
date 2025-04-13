using System;
using Embique.DocumentManagementApi.Enum;
using Embique.DocumentManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Embique.DocumentManagementApi.Data;

public class DocumentAppDbContext : DbContext
{
    public DbSet<DocumentModel> documentModels { get; set; }
    public DocumentAppDbContext(DbContextOptions<DocumentAppDbContext> options) : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DocumentModel>().HasData(
            new DocumentModel
                {
                    FileId = new Guid("9c1f5f7b-bf53-4f75-85e3-d9a1d5e4e2a1"),
                    Title = "Sample Document 1",
                    Author = "ABC",
                    FileType = FileType.Pdf,
                    DateOfCreation = new DateTime(2024, 04, 13, 0, 0, 0, DateTimeKind.Utc),
                    FilePath = "/uploads/sample1.pdf",
                    FileUploadedBy = "User1", // To Do
                    Metadata = "Data1"
                },
                new DocumentModel
                {
                    FileId = new Guid("5bfe2e5e-d8b7-4786-b2e9-22fd8fdcc022"),
                    Title = "Sample Document 2",
                    Author = "DEF",
                    FileType = FileType.Doc,
                    DateOfCreation = new DateTime(2024, 04, 13, 0, 0, 0, DateTimeKind.Utc),
                    FilePath = "/uploads/sample2.docx",
                    FileUploadedBy = "User2", // To Do
                    Metadata = "Data1"
                }
        );
    }
}
