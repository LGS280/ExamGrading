using IdentityService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Data.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);

        Task<User?> GetByUsernameAsync(string username);
        Task<List<string>> GetRolesByUserIdAsync(Guid userId);
    }
}
