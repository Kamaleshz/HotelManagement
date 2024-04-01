﻿using System.Security.Cryptography;
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
                if (userData != null && userData.HashKey != null && userDTO.Password != null && userData.UserPassword != null)
                {
                  var hmac = new HMACSHA512(userData.HashKey);
                  var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                  for (int i = 0; i < userPass.Length; i++)
                  {
                        if (userData == null || userData.HashKey == null || userData?.HashKey[i] == null)
                            throw new Exception("User data is null");
                        if (userPass[i] != userData.UserPassword[i])
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
                var user = await GetUserByMailId(updatePasswordDTO);
                if (user != null && updatePasswordDTO.HashKey != null && updatePasswordDTO != null)
                {
                    var hmac = new HMACSHA512(updatePasswordDTO.HashKey);
                    user.UserPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(updatePasswordDTO.Password));
                    await _commandRepository.UpdateUser(updatePasswordDTO);

                    return "Password Updated Successfully";
                }
                throw new NullReferenceException("Null");
            }
            catch (Exception ex) { throw new Exception(ex.Message) ; }
        }

        public async Task<string> UpdateUser(UserDTO updateUserDTO)
        {
            return await _commandRepository.UpdateUser(updateUserDTO);
        }
    }
}
