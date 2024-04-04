using Microsoft.EntityFrameworkCore;
using UserManagement.Interfaces.RepositoryInterfaces.QueryInterface;
using UserManagement.Models;

namespace UserManagement.Repository.QueryRepository
{
    public class QRoleRepository : IQRoleRepository
    {
        private readonly HotelManagementContext _context;

        public QRoleRepository(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Role>> GetRoles()
        {
            try
            {
                var role = await _context.Roles.ToListAsync();
                if(role != null)
                    return role;
                throw new NullReferenceException("No roles found");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
