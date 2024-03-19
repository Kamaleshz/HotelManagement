using UserManagement.Models;

namespace UserManagement.Interface.RepositoryInterface.QueryInterface
{
    public interface IQUserManagementRepository
    {
        public Task<ICollection<User>> GetAllUsers();
        public Task<ICollection<User>> GetUserById(int userId);
    }
}
