using UserManagement.Interface.RepositoryInterface.CommandInterface;
using UserManagement.Models;

namespace UserManagement.Repository.CommandRepository
{
    public class CUserManagementRepository : ICUserManagementRepository
    {
        private readonly UserManagementContext _context;

        public CUserManagementRepository(UserManagementContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " +  ex.Message);
            }
        }

        public Task<User> DeleteUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
