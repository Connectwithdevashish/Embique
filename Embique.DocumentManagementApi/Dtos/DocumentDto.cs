using System;
using Embique.DocumentManagementApi.Enum;

namespace Embique.DocumentManagementApi.Dtos;

public class DocumentDto
{
    public Guid FileId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string FilePath { get; set; }
    public DateTime DateOfCreation { get; set; }
    public FileType FileType { get; set; } = FileType.Txt;
    public string FileUploadedBy { get; set; }
    public string Metadata { get; set; }
}
