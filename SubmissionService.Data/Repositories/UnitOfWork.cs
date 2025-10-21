using SubmissionService.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SubmissionDbContext _context;
        public ISubmissionRepository Submissions { get; private set; }

        public UnitOfWork(SubmissionDbContext context)
        {
            _context = context;
            Submissions = new SubmissionRepository(_context);
            // Khởi tạo các repo khác ở đây
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
