using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamService.Data.Models
{
    [Table("Exams")]
    public class Exam
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Subject { get; set; }

        public DateTime? ExamDate { get; set; }

        [Required]
        public Guid CreatedByUserId { get; set; } // ID của Admin đã tạo kỳ thi

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
