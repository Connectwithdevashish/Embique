using System;

namespace Embique.DocumentManagementApi.Dtos;

public class DocumentUpdateDto
{
    public DateTime UpdatedOn { get; set; }
    public IFormFile UploadedFile { get; set; }
    public string Metadata { get; set; }  
}
