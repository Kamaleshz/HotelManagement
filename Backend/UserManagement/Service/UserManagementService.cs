using System.Security.Cryptography;
using System.Text;
using UserManagement.Interface.RepositoryInterface.CommandInterface;
using UserManagement.Interface.RepositoryInterface.QueryInterface;
using UserManagement.Interface.ServiceInterface;
using UserManagement.Interfaces.ServiceInterface;
using UserManagement.Models.DTO;

namespace UserManagement.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ICUserManagementRepository _commandRepository;
        private readonly IQUserManagementRepository _queryRepository;
        private readonly IUserTokenService _userTokenService;


        public UserManagementService (ICUserManagementRepository cUserManagementRepository, IQUserManagementRepository qUserManagementRepository, IUserTokenService userTokenService)
        {
            _commandRepository = cUserManagementRepository;
            _queryRepository = qUserManagementRepository;
            _userTokenService = userTokenService;
        }

        public async Task<string> DeleteUser(UserDTO deleteUserDTO)
        {
            return await _commandRepository.DeleteUser(deleteUserDTO);
        }

        public async Task<UserDTO> GetUserByMailId(UserDTO mailIdDTO)
        {
            try
            {
                var allUsers = await _queryRepository.GetAllUsers();
                var user = allUsers.FirstOrDefault(u => u.UserEmail ==  mailIdDTO.UserEmail && u.IsActive == true);
                if (user != null)
                {
                    mailIdDTO.UserId = user.UserId;
                    return mailIdDTO;
                }
                throw new NullReferenceException("No user found under this mail id");
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public async Task<UserDTO> Login(UserDTO userDTO)
        {
            userDTO = await GetUserByMailId(userDTO);
            var userData = await _queryRepository.GetUserById(userDTO.UserId);
            try
            {
                if (userData != null && userData.HashKey != null && userDTO.UserPassword != null)
                {
                  var hmac = new HMACSHA512(userData.HashKey);
                  var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.UserPassword));
                  for (int i = 0; i < userPass.Length; i++)
                  {
                        if (userData == null || userData.HashKey == null || userData?.HashKey[i] == null)
                            throw new Exception("User data is null");
                        if (userPass[i] != userData.HashKey[i])
                          throw new Exception("Wrong Password");
                  }
                  userDTO = new UserDTO();
                  userDTO.UserId = userData.UserId;
                  userDTO.FirstName = userData.FirstName;
                  userDTO.UserRole = userData.UserRole;
                  userDTO.Token = await _userTokenService.GenerateToken(userDTO);
                }
                return userDTO;
            }
            catch (Exception ex)
            {
                throw new NullReferenceException("Error:" + ex.Message);
            }
        }

        public Task<UserDTO> Resgister(UserDTO registerDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UpdatePasswordDTO> UpdatePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateUser(UserDTO updateUserDTO)
        {
            return await _commandRepository.UpdateUser(updateUserDTO);
        }
    }
}
