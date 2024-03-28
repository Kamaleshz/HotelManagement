using Microsoft.EntityFrameworkCore;
using UserManagement.Interface.RepositoryInterface.QueryInterface;
using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Repository.QueryRepository
{
    public class QUserManagementRepository : IQUserManagementRepository
    {
        private readonly HotelManagementContext _context;

        public QUserManagementRepository(HotelManagementContext context)
        {
            _context = context;
        }
        public async Task<ICollection<User>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                if (users != null)
                    return users;
                throw new NullReferenceException("No record found");
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                if (user != null)
                    return user;
                throw new NullReferenceException("No user found");
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public async Task<string> GetRoleNameById(UserDTO roleDTO)
        {
            return await _context.GetRoleNameById(roleDTO);
        }
    }
}
