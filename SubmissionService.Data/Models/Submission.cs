using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Data.Models
{
    [Table("Submissions")] // Chỉ định rõ tên bảng
    public class Submission
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ExamId { get; set; }

        [Required]
        public Guid UploadedByUserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string OriginalFileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ContentType { get; set; } = string.Empty;

        [Required]
        public long FileSizeBytes { get; set; }

        [Required]
        public string StoragePath { get; set; } = string.Empty; // Đường dẫn trên Supabase

        [Required]
        public DateTime UploadedAt { get; set; }
    }
}
