using BCrypt.Net;
using IdentityService.Business.Interfaces;
using IdentityService.Business.ServiceModels.Requests;
using IdentityService.Data.Interfaces;
using IdentityService.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }
        public async Task<string?> LoginUserAsync(LoginRequest loginRequest)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(loginRequest.Username);

            // Kiểm tra user và mật khẩu
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                return null; // Sai thông tin đăng nhập
            }

            // Lấy danh sách vai trò của user
            var roles = await _unitOfWork.Users.GetRolesByUserIdAsync(user.Id);

            // Nếu đúng, tạo JWT Token VỚI CÁC VAI TRÒ
            return CreateJwtToken(user, roles);
        }


        public async Task<bool> RegisterUserAsync(RegisterRequest registerDto)
        {
            if (await _unitOfWork.Users.GetByUsernameAsync(registerDto.Username) != null)
            {
                return false; // Username đã tồn tại
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                CreatedAt = DateTime.UtcNow,
                UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        RoleId = 2 // Giả sử RoleId 2 là "Teacher"
                    }
                }
            };

            await _unitOfWork.Users.AddUser(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private string CreateJwtToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                // Thêm các claims khác nếu cần, ví dụ: new Claim(ClaimTypes.Role, "Admin")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
