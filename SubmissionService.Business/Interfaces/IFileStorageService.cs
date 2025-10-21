using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SubmissionService.Data.Interfaces
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Tải một file lên dịch vụ lưu trữ.
        /// </summary>
        /// <param name="stream">Luồng dữ liệu của file.</param>
        /// <param name="storagePath">Đường dẫn duy nhất để lưu file trên storage (ví dụ: "submissions/examId/guid.pdf").</param>
        /// <param name="contentType">Loại nội dung của file (ví dụ: "application/pdf").</param>
        /// <returns>Đường dẫn công khai hoặc key của file đã được lưu.</returns>
        Task<string> UploadFileAsync(Stream stream, string storagePath, string contentType);

        /// <summary>
        /// Tải một file từ dịch vụ lưu trữ về.
        /// </summary>
        /// <param name="storagePath">Đường dẫn của file trên storage.</param>
        /// <returns>Một MemoryStream chứa dữ liệu của file.</returns>
        Task<MemoryStream> DownloadFileAsync(string storagePath);


        /// <summary>
        /// Gets a public URL for a file, which can be used to view or download it directly.
        /// </summary>
        /// <param name="storagePath">The path of the file.</param>
        /// <returns>The public URL of the file.</returns>
        string GetPublicUrl(string storagePath);

    }
}
