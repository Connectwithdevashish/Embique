using System;

namespace Embique.DocumentManagementApi.Dtos;

public class ResponseDto
{
    public bool IsSucess { get; set; } = false;
    public object Result { get; set; }
    public string ErrorMessage { get; set; } = "";
}
