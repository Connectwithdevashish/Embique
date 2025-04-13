using Embique.DocumentManagementApi.Data;
using Embique.DocumentManagementApi.Dtos;
using Embique.DocumentManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Embique.DocumentManagementApi.Controllers
{
    [ApiController]
    // [Authorize("Admin")]
    [Route("api/DocumentUploadApi")]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentAppDbContext _db;
        private readonly IDocumentService _documentService;
        private ResponseDto _responseDto;

        public DocumentsController(DocumentAppDbContext db,
        IDocumentService documentService)
        {
            _db = db;
            _documentService = documentService;
            _responseDto = new ResponseDto();   
        }
        [HttpGet("GetAll")]
        public async Task<ResponseDto> GetAllDoc()
        {
            try
            {
                _responseDto.Result = await _documentService.GetAllDocumentsAsync();
                _responseDto.IsSucess = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.ErrorMessage = ex.Message;
            }
            return _responseDto;
        }
        [HttpGet("GetById")]
        public async Task<ResponseDto> GetDocById(Guid guidId)
        {
            try
            {
                _responseDto.Result = await _documentService.GetDocumentsByIdAsync(guidId);
                _responseDto.IsSucess = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.ErrorMessage = ex.Message;
            }
            return _responseDto;
        }
        [HttpPost("DocDelete")]
        public async Task<ResponseDto> DeleteDocument([FromForm] DocumentUploadDto documentUploadDto)
        {
            try
            {
                _responseDto.Result = await _documentService.DeleteDocumentAsync(documentUploadDto);
                _responseDto.IsSucess = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.ErrorMessage = ex.Message;
            }
            return _responseDto;
        }
        [HttpPost("DocUpdate")]
        public async Task<ResponseDto> DocumentUpdate([FromForm] DocumentUpdateDto documentUpdateDto)
        {
            try
            {
                _responseDto.Result = await _documentService.UpdateDocumentAsync(documentUpdateDto);
                _responseDto.IsSucess = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.ErrorMessage = ex.Message;
            }
            return _responseDto;
        }
        [HttpPost("DocUpload")]
        public async Task<ResponseDto> UploadDocument([FromForm] DocumentUploadDto documentUploadDto)
        {
            try
            {
                _responseDto.Result = await _documentService.UploadDocumentAsync(documentUploadDto);
                _responseDto.IsSucess = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.ErrorMessage = ex.Message;
            }
            return _responseDto;
        }
    }
}
