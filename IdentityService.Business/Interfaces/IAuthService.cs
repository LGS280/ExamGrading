using IdentityService.Business.ServiceModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Business.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterRequest registerDto);
        Task<string?> LoginUserAsync(LoginRequest loginDto);

    }
}
