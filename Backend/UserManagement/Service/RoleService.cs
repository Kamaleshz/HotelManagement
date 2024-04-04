using UserManagement.Interfaces.RepositoryInterfaces.QueryInterface;
using UserManagement.Interfaces.ServiceInterface;
using UserManagement.Models;

namespace UserManagement.Service
{
    public class RoleService : IRoleService
    {
        private readonly IQRoleRepository _roleRepository;

        public RoleService(IQRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<ICollection<Role>> GetRoles()
        {
            return _roleRepository.GetRoles();
        }
    }
}
