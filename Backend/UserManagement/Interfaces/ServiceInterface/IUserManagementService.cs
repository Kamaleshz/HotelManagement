using UserManagement.Models;

namespace UserManagement.Interface.ServiceInterface
{
    public interface IUserManagementService
    {
        public Task<User> CreateUser(User user);
        public Task<ICollection<User>> GetAllUsers();
        public Task<ICollection<User>> GetUsersById(int id);
        public Task<User> UpdateUser(User user);
        public Task<User> DeleteUser(User user);
    }
}
