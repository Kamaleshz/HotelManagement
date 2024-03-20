using UserManagement.Models;

namespace UserManagement.Interface.RepositoryInterface.CommandInterface
{
    public interface ICUserManagementRepository
    {
        public Task<string> CreateUser(User user);

        public Task<string> UpdateUser(User user);

        public Task<string> DeleteUser(int UserId);
    }
}
