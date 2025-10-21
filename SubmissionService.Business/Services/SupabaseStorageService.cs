using SubmissionService.Data.Interfaces;
using Supabase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Business.Services
{
    public class SupabaseStorageService : IFileStorageService
    {
        private readonly string _supabaseUrl;
        private readonly string _supabaseKey;
        private readonly string _bucketName;

        public SupabaseStorageService(string supabaseUrl, string supabaseKey, string bucketName)
        {
            _supabaseUrl = supabaseUrl ?? throw new ArgumentNullException(nameof(supabaseUrl));
            _supabaseKey = supabaseKey ?? throw new ArgumentNullException(nameof(supabaseKey));
            _bucketName = bucketName ?? throw new ArgumentNullException(nameof(bucketName));
        }

        public async Task<MemoryStream> DownloadFileAsync(string storagePath)
        {
            var client = new Client(_supabaseUrl, new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {_supabaseKey}" }
                });

            // Sửa lỗi ở đây:
            // Cung cấp `null` cho tham số thứ hai để giải quyết sự không rõ ràng.
            var bytes = await client.From(_bucketName).Download(storagePath, null);

            if (bytes == null)
            {
                throw new FileNotFoundException("The specified file was not found in Supabase Storage.", storagePath);
            }

            return new MemoryStream(bytes);

        }


        public string GetPublicUrl(string storagePath)
        {
            var client = new Client(_supabaseUrl, new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {_supabaseKey}" }
            });

            // Phương thức mới là `GetPublicUrl`
            var url = client.From(_bucketName).GetPublicUrl(storagePath);
            return url;
        }


        public async Task<string> UploadFileAsync(Stream stream, string storagePath, string contentType)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            // Cách gọi mới: Sử dụng phương thức tĩnh của `Supabase.Storage.Client`
            // Client được tạo ngầm bên trong các phương thức này.
            var client = new Client(_supabaseUrl, new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {_supabaseKey}" }
            });

            // Phương thức mới là `Upload` nhưng nhận tham số khác
            await client.From(_bucketName).Upload(fileBytes, storagePath, new Supabase.Storage.FileOptions
            {
                CacheControl = "3600", // Thời gian cache (ví dụ 1 giờ)
                Upsert = true,
                ContentType = contentType
            });

            return storagePath;
        }
    }
}
