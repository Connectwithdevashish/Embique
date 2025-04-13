using System;

namespace Embique.DocumentManagementApi.Dtos;

public class DocumentUploadDto
{
    public DateTime UUploadedOn { get; set; }
    public IFormFile UploadedFile { get; set; }
    public string FileUploadedBy { get; set; }
}
