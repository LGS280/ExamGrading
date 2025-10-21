namespace SubmissionService.Data.Models
{
    public class SubmissionBatch
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public string? OriginalFileName { get; set; }
        public int TotalFiles { get; set; }
        public Guid UploadedByUserId { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public virtual Exam Exam { get; set; } = null!;
        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
