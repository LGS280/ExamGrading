using ExamService.Business.ServiceModels.Requests;
using ExamService.Business.ServiceModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamService.Business.Interfaces
{
    public interface IExamManagementService
    {
        Task<ExamResponse?> CreateExamAsync(CreateExam examDto, Guid creatorId);
        Task<ExamResponse?> GetExamByIdAsync(Guid id);
        Task<IEnumerable<ExamResponse>> GetAllExamsAsync();
    }
}
