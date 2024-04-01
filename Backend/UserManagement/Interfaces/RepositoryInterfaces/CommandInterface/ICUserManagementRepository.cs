using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Interface.RepositoryInterface.CommandInterface
{
    public interface ICUserManagementRepository
    {
        public Task<User> CreateUser(User userDTO);

        public Task<string> UpdateUser(UserDTO updateUserDTO);

        public Task<string> DeleteUser(UserDTO deleteUserDTO);
    }
}
