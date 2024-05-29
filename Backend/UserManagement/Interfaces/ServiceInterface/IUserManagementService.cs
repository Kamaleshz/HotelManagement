using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Interface.ServiceInterface
{
    public interface IUserManagementService
    {
        public Task<UserDTO> Login(UserDTO userDTO);
        public Task<UserDTO> GetUserByMailId(UserDTO mailIdDTO);
        public Task<UserDTO> Resgister(RegisterDTO registerDTO);
        public Task<string> UpdateUser(UserDTO updateUserDTO);
        public Task<string> ChangePassword(UpdatePasswordDTO updatePasswordDTO);
        public Task<string> DeleteUser(UserDTO deleteUserDTO);
    }
}