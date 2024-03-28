using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Interface.ServiceInterface
{
    public interface IUserManagementService
    {
        public Task<UserDTO> Login(UserDTO userDTO);
        public Task<UserDTO> GetUserByMailId(UserDTO mailIdDTO);
        public Task<UserDTO> Resgister(UserDTO registerDTO);
        public Task<string> UpdateUser(UserDTO updateUserDTO);
        public Task<UpdatePasswordDTO> UpdatePassword(UpdatePasswordDTO updatePasswordDTO);
        public Task<string> DeleteUser(UserDTO deleteUserDTO);
    }
}