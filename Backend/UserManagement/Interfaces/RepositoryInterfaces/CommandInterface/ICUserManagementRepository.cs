using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Interface.RepositoryInterface.CommandInterface
{
    public interface ICUserManagementRepository
    {
        public Task<string> CreateUser(UserDTO userDTO);

        public Task<string> UpdateUser(UpdateUserDTO updateUserDTO);

        public Task<string> DeleteUser(DeleteUserDTO deleteUserDTO);
    }
}
