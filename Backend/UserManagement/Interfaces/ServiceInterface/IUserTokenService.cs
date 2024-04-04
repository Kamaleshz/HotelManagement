using UserManagement.Models.DTO;

namespace UserManagement.Interfaces.ServiceInterface
{
    public interface IUserTokenService
    {
        public Task<string> GenerateToken(UserDTO loginDTO);

    }
}
