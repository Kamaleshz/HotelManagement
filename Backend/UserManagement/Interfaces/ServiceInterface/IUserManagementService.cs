using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Interface.ServiceInterface
{
    public interface IUserManagementService
    {
        public Task<LoginDTO> Login(LoginDTO loginDTO);
        public Task<RegisterDTO> Resgister(RegisterDTO registerDTO);
        public Task<UpdateUserDTO> UpdateUser(UpdateUserDTO updateUserDTO);
        public Task<User> UpdatePassword(User user);
        public Task<User> DeleteUser(User user);
    }
}
