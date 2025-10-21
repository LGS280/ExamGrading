using ExamService.Data;
using ExamService.Data.Interfaces;
using ExamService.Data.Models;
using ExamService.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(ExamDbContext context) : base(context)
        {
        }
    }
}
