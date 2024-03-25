using UserManagement.Interface.RepositoryInterface.CommandInterface;
using UserManagement.Interface.RepositoryInterface.QueryInterface;
using UserManagement.Interface.ServiceInterface;
using UserManagement.Models.DTO;
using UserManagement.Repository.CommandRepository;

namespace UserManagement.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ICUserManagementRepository _commandRepository;
        private readonly IQUserManagementRepository _queryRepository;

        public UserManagementService (ICUserManagementRepository cUserManagementRepository, IQUserManagementRepository qUserManagementRepository)
        {
            _commandRepository = cUserManagementRepository;
            _queryRepository = qUserManagementRepository;
        }

        public Task<DeleteUserDTO> DeleteUser(DeleteUserDTO deleteUserDTO)
        {
            throw new NotImplementedException();
        }

        public Task<LoginDTO> Login(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterDTO> Resgister(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UpdatePasswordDTO> UpdatePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserDTO> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            throw new NotImplementedException();
        }
    }
}
