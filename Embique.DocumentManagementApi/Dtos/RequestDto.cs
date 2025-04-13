using System;
using Embique.DocumentManagementApi.Enum;

namespace Embique.DocumentManagementApi.Dtos;

public class RequestDto
{
    public Object ObjectInRequest { get; set; }
    public bool IsSucess { get; set; } = false;
    public string Error { get; set; } = "";
    public string FileUploadedBy { get; set; }
}
