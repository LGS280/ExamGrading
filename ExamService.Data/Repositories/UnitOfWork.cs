using ExamService.Data.Interfaces;
using IdentityService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamService.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamDbContext _context;
        public IExamRepository Exams { get; private set; }

        public UnitOfWork(ExamDbContext context)
        {
            _context = context;
            Exams = new ExamRepository(_context);
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
