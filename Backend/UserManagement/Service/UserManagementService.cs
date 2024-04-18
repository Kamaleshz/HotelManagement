using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
                    return mailIdDTO;
                }
                throw new NullReferenceException("No user found with this email");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<UserDTO> Login(UserDTO userDTO)
        {
            userDTO = await GetUserByMailId(userDTO);
            try
            {
                if (userDTO.HashKey == null)
                    throw new NullReferenceException("HashKey cannot be retrieved");
                if (userDTO.Password == null)
                    throw new NullReferenceException("Password cannot be null");

                var hmac = new HMACSHA512(userDTO.HashKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));

                if (userDTO.UserPassword == null || userPass.Length != userDTO.UserPassword.Length)
                    throw new Exception("Password length mismatch");

                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userDTO == null)
                        throw new Exception("User data cannot be retrieved");
                    if (userPass[i] != userDTO.UserPassword[i])
                        throw new Exception("Wrong Password");
                }
                userDTO.Token = await _userTokenService.GenerateToken(userDTO);
                return userDTO;
            }
            catch (Exception ex) {  throw new Exception(ex.Message) ; }
        }


        public async Task<UserDTO> Resgister(RegisterDTO registerDTO)
        {
            try
            {
                UserDTO userDTO = new UserDTO();
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
                        userDTO.UserId = result.UserId;
                        userDTO.FirstName = result.FirstName;
                        userDTO.LastName = result.LastName;
                        userDTO.UserEmail = result.UserEmail;
                        userDTO.UserPhoneNumber = result.UserPhoneNumber;
                        userDTO.UserRole = result.UserRole;
                        userDTO.Token = await _userTokenService.GenerateToken(userDTO);
                    }
                    if(userDTO == null)
                        throw new NullReferenceException("Result cannot be retrieved");
                    return userDTO;
                throw new NullReferenceException("Password cannot be null");
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<string> UpdatePassword(UserDTO updatePasswordDTO)
        {
            try
            {
                var user = await _queryRepository.GetUserById(updatePasswordDTO.UserId);
                if (user != null)
                {
                    if(user.HashKey == null)
                        throw new NullReferenceException("Haskey cannot be null.");
                    if(updatePasswordDTO.Password == null)
                        throw new NullReferenceException("Password Cannot be null");

                    var hmac = new HMACSHA512(user.HashKey);
                    user.UserPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(updatePasswordDTO.Password));
                    await _commandRepository.UpdateUser(user);

                    return "Password Updated Successfully";
                }
                throw new NullReferenceException("User not Found. Cannot update the Password");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<string> UpdateUser(UserDTO updateUserDTO)
        {
            var user = _mapper.Map<User>(updateUserDTO);
            return await _commandRepository.UpdateUser(user);
        }
    }
}
