using UserManagement.Models;

namespace UserManagement.Interface.RepositoryInterface.CommandInterface
{
    public interface ICUserManagementRepository
    {
        public Task<User> CreateUser(User user);

        public Task<User> UpdateUser(User user);

        public Task<User> DeleteUser(int UserId);
    }
}
