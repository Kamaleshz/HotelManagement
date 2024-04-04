using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Interface.RepositoryInterface.QueryInterface
{
    public interface IQUserManagementRepository
    {
        public Task<ICollection<User>> GetAllUsers();
        public Task<User> GetUserById(int userId);
        public Task<string> GetRoleNameById(UserDTO roleDTO);
    }
}
