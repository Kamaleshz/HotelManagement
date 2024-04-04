using UserManagement.Models;

namespace UserManagement.Interfaces.ServiceInterface
{
    public interface IRoleService
    {
        public Task<ICollection<Role>> GetRoles();
    }
}
