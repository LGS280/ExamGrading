using SubmissionService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Data.Interfaces
{
    public interface ISubmissionRepository : IGenericRepository<Submission>
    {
        Task<IEnumerable<Submission>> GetSubmissionsByExamIdAsync(Guid examId);
        Task AddSubmission(Submission submission);


    }
}
