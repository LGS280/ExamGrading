using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamService.Business.ServiceModels.Requests
{
    public class CreateExam
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Subject { get; set; }

        public DateTime? ExamDate { get; set; }
    }
}
