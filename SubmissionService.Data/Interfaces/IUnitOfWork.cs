using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISubmissionRepository Submissions { get; }
        Task<int> SaveChangesAsync();
    }
}
