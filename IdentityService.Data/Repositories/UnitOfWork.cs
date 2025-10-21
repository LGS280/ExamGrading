using IdentityService.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _context;
        public IUserRepository Users { get; private set; }

        public UnitOfWork(IdentityDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
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
