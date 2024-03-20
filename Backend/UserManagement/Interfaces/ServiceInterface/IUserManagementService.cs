using UserManagement.Models;

namespace UserManagement.Interface.ServiceInterface
{
    public interface IUserManagementService
    {
        public Task<User> Login(User user);
        public Task<User> Resgister(User user);
        public Task<User> UpdateUser(User user);
        public Task<User> UpdatePassword(User user);
    }
}
