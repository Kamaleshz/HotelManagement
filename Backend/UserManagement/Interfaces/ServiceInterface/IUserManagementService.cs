using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Interface.ServiceInterface
{
    public interface IUserManagementService
    {
        public Task<LoginDTO> Login(LoginDTO loginDTO);
        public Task<RegisterDTO> Resgister(RegisterDTO registerDTO);
        public Task<UpdateUserDTO> UpdateUser(UpdateUserDTO updateUserDTO);
        public Task<UpdatePasswordDTO> UpdatePassword(UpdatePasswordDTO updatePasswordDTO);
        public Task<DeleteUserDTO> DeleteUser(DeleteUserDTO deleteUserDTO);
    }
}
