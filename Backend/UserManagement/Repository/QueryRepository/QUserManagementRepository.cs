using UserManagement.Interface.RepositoryInterface.QueryInterface;
using UserManagement.Models;

namespace UserManagement.Repository.QueryRepository
{
    public class QUserManagementRepository : IQUserManagementRepository
    {
        public Task<ICollection<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
