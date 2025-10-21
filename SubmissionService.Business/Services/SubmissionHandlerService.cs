using SubmissionService.Business.Interfaces;
using SubmissionService.Business.ServiceModel.Responses;
using SubmissionService.Data.Interfaces;
using SubmissionService.Data.Models;
using SubmissionService.Data.Repositories;

namespace SubmissionService.Business.Services
{
    public class SubmissionHandlerService : ISubmissionHandlerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _storageService;
        private static readonly List<string> AllowedContentTypes = new()
        {
            "application/pdf",
            "application/zip", // <--- CHỈ CÓ ZIP
            "application/x-zip-compressed",
            "application/msword", // .doc
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // .docx
            "application/vnd.ms-excel", // .xls
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // .xlsx
            "application/x-rar-compressed",
            "application/rar",              // Một loại MIME khác
            "application/x-compressed"
        };

        public SubmissionHandlerService(IUnitOfWork unitOfWork, IFileStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _storageService = storageService;
        }

        public async Task<SubmissionResponse> HandleSubmissionAsync(Stream fileStream, string originalFileName, string contentType, long fileSize, Guid examId, Guid uploaderId)
        {
            if (!AllowedContentTypes.Contains(contentType.ToLower()))
            {
                throw new ArgumentException($"File type '{contentType}' is not permitted.");
            }

            //Tạo đường dẫn trên Supabase Storage

            var fileExtension = Path.GetExtension(originalFileName).ToLower();
            var cleanFileName = $"{Guid.NewGuid()}{fileExtension}";

            //var storagePath = $"submissions/{examId}/{cleanFileName}";
            var storagePath = Path.Combine("submissions", examId.ToString(), cleanFileName)
                .Replace("\\", "/")        // chuẩn hóa separator
                .TrimStart('/');

            Console.WriteLine($"Uploading to Supabase with path: {storagePath}");

            //Upload file lên Supabase
            await _storageService.UploadFileAsync(fileStream, storagePath, contentType);

            //Tạo đối tượng Entity để lưu vào DB
            var submission = new Submission
            {
                Id = Guid.NewGuid(),
                ExamId = examId,
                UploadedByUserId = uploaderId,
                OriginalFileName = originalFileName,
                ContentType = contentType,
                FileSizeBytes = fileSize,
                StoragePath = storagePath,
                UploadedAt = DateTime.UtcNow
            };

            //Lưu metadata vào PostgreSQL (Neon) thông qua Repository
            await _unitOfWork.Submissions.AddSubmission(submission);
            await _unitOfWork.SaveChangesAsync();

            //Trả về DTO
            return new SubmissionResponse
            {   
                SubmissionId = submission.Id,
                FileName = submission.OriginalFileName,
                StoragePath = submission.StoragePath,
                FileSizeBytes = submission.FileSizeBytes
            };
        }

    }
}
