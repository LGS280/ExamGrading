using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Data.Models
{
    public class Exam
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Subject { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<SubmissionBatch> SubmissionBatches { get; set; } = new List<SubmissionBatch>();
    }
}
