using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Interface.RepositoryInterface.QueryInterface;
using UserManagement.Interfaces.ServiceInterface;
using UserManagement.Models.DTO;
using UserManagement.Repository.QueryRepository;

namespace UserManagement.Service
{
    public class UserTokenService : IUserTokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IQUserManagementRepository _repository;

        public UserTokenService(IConfiguration configuration, IQUserManagementRepository qUserManagementRepository)
        {
            string? tokenKey = configuration["TokenKey"];
            if (tokenKey == null)
            {
                throw new ArgumentNullException(nameof(configuration), "Token key configuration is missing.");
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            _repository = qUserManagementRepository;
        }
        public async Task<string> GenerateToken(UserDTO loginDTO)
        {
            string roleName = await _repository.GetRoleNameById(loginDTO);
            try
            {
                if (loginDTO.UserRole == null)
                    throw new NullReferenceException("No role for the id passed");
                var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.NameId,loginDTO.UserId.ToString()),
                        new Claim(ClaimTypes.Role, roleName)
                    };
                var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(3),
                    SigningCredentials = cred
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var myToken = tokenHandler.CreateToken(tokenDescription);
                string token = tokenHandler.WriteToken(myToken);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
            
        }
    }
}
