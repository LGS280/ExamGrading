using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamService.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IExamRepository Exams { get; }
        Task<int> SaveChangesAsync();
    }
}
