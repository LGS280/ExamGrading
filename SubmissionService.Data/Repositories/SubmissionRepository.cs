using Microsoft.EntityFrameworkCore;
using SubmissionService.Data.Interfaces;
using SubmissionService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Data.Repositories
{
    public class SubmissionRepository : GenericRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(SubmissionDbContext context) : base(context)
        {
        }

        public async Task AddSubmission(Submission submission)
        {
            await _context.Submissions.AddAsync(submission);
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByExamIdAsync(Guid examId)
        {
            return await _dbSet
                .Where(s => s.ExamId == examId)
                .OrderByDescending(s => s.UploadedAt)
                .ToListAsync();
        }
    }
}
