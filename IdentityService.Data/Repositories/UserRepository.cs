using IdentityService.Data.Interfaces;
using IdentityService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IdentityDbContext context) : base(context)
        {
        }

        public async Task AddUser(User user)
        {
            await _dbSet.AddAsync(user);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {

            return await _dbSet.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<List<string>> GetRolesByUserIdAsync(Guid userId)
        {
            return await _context.UserRoles
            .Where(ur => ur.UserId == userId) 
            .Select(ur => ur.Role.Name)
            .ToListAsync();

        }

    }
}
