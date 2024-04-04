using UserManagement.Models;

namespace UserManagement.Interfaces.RepositoryInterfaces.QueryInterface
{
    public interface IQRoleRepository
    {
        public Task<ICollection<Role>> GetRoles();
    }
}
