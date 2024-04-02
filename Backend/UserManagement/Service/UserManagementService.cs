using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using UserManagement.Interface.RepositoryInterface.CommandInterface;
using UserManagement.Interface.RepositoryInterface.QueryInterface;
using UserManagement.Interface.ServiceInterface;
using UserManagement.Interfaces.ServiceInterface;
using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ICUserManagementRepository _commandRepository;
        private readonly IQUserManagementRepository _queryRepository;
        private readonly IUserTokenService _userTokenService;
        private readonly IMapper _mapper;


        public UserManagementService (ICUserManagementRepository cUserManagementRepository, IQUserManagementRepository qUserManagementRepository, IUserTokenService userTokenService, IMapper mapper)
        {
            _commandRepository = cUserManagementRepository;
            _queryRepository = qUserManagementRepository;
            _userTokenService = userTokenService;
            _mapper = mapper;
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
                    var users = _mapper.Map<User>(mailIdDTO);

                    mailIdDTO.UserId = user.UserId;
                    mailIdDTO.FirstName = user.FirstName;
                    mailIdDTO.LastName = user.LastName;
                    mailIdDTO.UserEmail = user.UserEmail;
                    mailIdDTO.UserPassword = user.UserPassword;
                    mailIdDTO.HashKey = user.HashKey;
                    mailIdDTO.UserPhoneNumber = user.UserPhoneNumber;
                    mailIdDTO.UserRole = user.UserRole;
                    mailIdDTO.IsActive = user.IsActive;
                    mailIdDTO.CreatedBy = user.CreatedBy;
                    mailIdDTO.CreatedOn = user.CreatedOn;
                    mailIdDTO.ModifiedBy = user.FirstName;
                    mailIdDTO.ModifiedOn = user.ModifiedOn;
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
                if (userData != null && userDTO.HashKey != null && userDTO.Password != null && userData.UserPassword != null)
                {
                  var hmac = new HMACSHA512(userData.HashKey);
                  var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                  for (int i = 0; i < userPass.Length; i++)
                  {
                        if (userData == null || userData.HashKey == null)
                            throw new Exception("User data is null");
                        if (userPass[i] != userData.UserPassword[i])
                          throw new Exception("Wrong Password");
                  }
                  userDTO.Token = await _userTokenService.GenerateToken(userDTO);
                }
                return userDTO;
            }
            catch (Exception ex)
            {
                throw new NullReferenceException("Error:" + ex.Message);
            }
        }

        public async Task<UserDTO> Resgister(RegisterDTO registerDTO)
        {
            try
            {
                UserDTO userDTO = null;
                var hmac = new HMACSHA512();
                if (registerDTO.Password != null)
                    registerDTO.UserPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
                    registerDTO.HashKey = hmac.Key;

                    if (registerDTO.UserRole == 2)
                        registerDTO.IsActive = false;
                    registerDTO.IsActive = true;

                var result = await _commandRepository.CreateUser(registerDTO);

                    if(result != null)
                    {
                        userDTO = new UserDTO();
                        userDTO.FirstName = result.FirstName;
                        userDTO.LastName = result.LastName;
                        userDTO.UserRole = result.UserRole;
                        userDTO.Token = await _userTokenService.GenerateToken(userDTO);
                    }
                    return userDTO;
                throw new NullReferenceException("Password connot be null");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdatePassword(UserDTO updatePasswordDTO)
        {
            try
            {
                var user = await _queryRepository.GetUserById(updatePasswordDTO.UserId);
                if (user != null)
                {
                    var hmac = new HMACSHA512(user.HashKey);
                    user.UserPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(updatePasswordDTO.Password));
                    await _commandRepository.UpdateUser(user);

                    return "Password Updated Successfully";
                }
                throw new NullReferenceException("Null");
            }
            catch (Exception ex) { throw new Exception(ex.Message) ; }
        }

        public async Task<string> UpdateUser(UserDTO updateUserDTO)
        {
            var user = _mapper.Map<User>(updateUserDTO);
            return await _commandRepository.UpdateUser(user);
        }
    }
}
