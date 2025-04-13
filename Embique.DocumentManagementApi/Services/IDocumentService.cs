using System;
using Embique.DocumentManagementApi.Dtos;
using Embique.DocumentManagementApi.Models;

namespace Embique.DocumentManagementApi.Services;

public interface IDocumentService
{
    Task<IEnumerable<DocumentModel>> GetAllDocumentsAsync();
    Task<DocumentModel> GetDocumentsByIdAsync(Guid guidId);
    Task<DocumentModel> UploadDocumentAsync(DocumentUploadDto documentUploadDto);
    Task<bool> DeleteDocumentAsync(DocumentUploadDto documentUploadDto);
    Task<bool> UpdateDocumentAsync(DocumentUpdateDto documentUpdateDto);
}
