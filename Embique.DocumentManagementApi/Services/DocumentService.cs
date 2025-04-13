using Embique.DocumentManagementApi.Data;
using Embique.DocumentManagementApi.Dtos;
using Embique.DocumentManagementApi.Enum;
using Embique.DocumentManagementApi.Extension;
using Embique.DocumentManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Embique.DocumentManagementApi.Services;

public class DocumentService(DocumentAppDbContext db, ILogger<DocumentService> logger,
    IConfiguration configuration) : IDocumentService
{
    public async Task<IEnumerable<DocumentModel>> GetAllDocumentsAsync()
    {
        try
        {
            logger.GetInfoLoggingGoing("Gettig all Document");
            return await db.documentModels.ToListAsync();    
        }
        catch (Exception ex)
        {
            logger.GetExceptionLoggingGoing($"We have Exception - {ex.Message}");
            return new List<DocumentModel>();
        }
    }

    public async Task<DocumentModel> GetDocumentsByIdAsync(Guid guidId)
    {
        try
        {
            logger.GetInfoLoggingGoing($"Gettig Document for id -> {guidId}");
            return await db.documentModels.FirstOrDefaultAsync(o => o.FileId == guidId);
        }
        catch (Exception ex)
        {
            logger.GetExceptionLoggingGoing($"We have Exception - {ex.Message}");
            return new DocumentModel();
        }
    }

    public async Task<bool> UpdateDocumentAsync(DocumentUpdateDto documentUpdateDto)
    {
        try
        {
            if(documentUpdateDto == null || documentUpdateDto.UploadedFile.Length == 0) return false;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(documentUpdateDto.UploadedFile.FileName);
            var fileNameExtension = Path.GetExtension(documentUpdateDto.UploadedFile.FileName);
            var fullFileName = Path.GetFileName(documentUpdateDto.UploadedFile.FileName);
            var filePath = configuration.GetValue<string>("FilePath");
            // Assume file name is always guid followed by extension

            if( await db.documentModels.AnyAsync(e => e.FileId.ToString() == fileNameWithoutExtension ) == false) {
                logger.GetExceptionLoggingGoing($"File with this name doesn't exist in database");
                return false;
            }

            logger.GetExceptionLoggingGoing($"Getting File Data using Guid Id");
            
            var existingFile = await db.documentModels.FirstOrDefaultAsync(e => e.FileId.ToString() == fileNameWithoutExtension);
            if(File.Exists(filePath)){
                File.Delete(filePath);
            }
            using(var stream = new FileStream(fullFileName, FileMode.Create)){
                await documentUpdateDto.UploadedFile.CopyToAsync(stream);
            }
            
            var UpdateInFile = new DocumentModel{
                Author = existingFile.Author,
                DateOfCreation = documentUpdateDto.UpdatedOn,
                FileId = new Guid(fileNameWithoutExtension),
                FilePath = filePath,
                FileType = GetExtension(fileNameExtension),
                FileUploadedBy = "User1" // Replace by User during Authentication
            };
            db.documentModels.Update(UpdateInFile);
            
            logger.GetExceptionLoggingGoing($"Saving File data to database");
            
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.GetExceptionLoggingGoing($"We have Exception - {ex.Message}");
            return false;
        }

    }

    private FileType GetExtension(string extension)
    {
        return extension.ToLower()
        switch
        {
            ".pdf" => FileType.Pdf,
            ".txt" => FileType.Txt,
            ".ppt" => FileType.Ppt,
            ".doc" => FileType.Doc,
            _ => FileType.Txt
        };
    }

    public async Task<DocumentModel> UploadDocumentAsync(DocumentUploadDto documentUploadDto)
    {
        var DocumentModelObject = new DocumentModel();
        try
        {
            if(documentUploadDto == null || documentUploadDto.UploadedFile.Length == 0) return DocumentModelObject;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(documentUploadDto.UploadedFile.FileName);
            var fileNameExtension = Path.GetExtension(documentUploadDto.UploadedFile.FileName);
            var fullFileName = Path.GetFileName(documentUploadDto.UploadedFile.FileName);
            var filePath = configuration.GetValue<string>("FilePath");
            var existingFile = await db.documentModels.FirstOrDefaultAsync(e => e.FileId.ToString() == fileNameWithoutExtension);
            
            // Assume file name is always guid followed by extension

            if( await db.documentModels.AnyAsync(e => e.FileId.ToString() == fileNameWithoutExtension)) {
                logger.GetExceptionLoggingGoing($"File with this name already in database");
                return DocumentModelObject;
            }

            using(var stream = new FileStream(filePath, FileMode.CreateNew)){
                await documentUploadDto.UploadedFile.CopyToAsync(stream);
            }

            var UpdateInFile = new DocumentModel{
                Author = existingFile.Author,
                DateOfCreation = documentUploadDto.UUploadedOn,
                FileId = new Guid(fileNameWithoutExtension),
                FilePath = filePath,
                FileType = GetExtension(fileNameExtension),
                FileUploadedBy = "User1" // Replace by User during Authentication
            };
            db.documentModels.Update(UpdateInFile);
            
            logger.GetExceptionLoggingGoing($"Saving File data to database");
            
            await db.SaveChangesAsync();
            return UpdateInFile;
        }
        catch (Exception ex)
        {
            logger.GetExceptionLoggingGoing($"We have Exception - {ex.Message}");
            return DocumentModelObject;
        }
    }
    
    public async Task<bool> DeleteDocumentAsync(DocumentUploadDto documentUploadDto)
    {
        if(documentUploadDto == null || documentUploadDto.UploadedFile.Length == 0) return false;
        var filePath = configuration.GetValue<string>("FilePath");
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }
}
