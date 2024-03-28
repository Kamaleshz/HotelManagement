﻿using Microsoft.EntityFrameworkCore;
using UserManagement.Interface.RepositoryInterface.CommandInterface;
using UserManagement.Models;
using UserManagement.Models.DTO;
using UserManagement.Helper;
using AutoMapper;

namespace UserManagement.Repository.CommandRepository
{
    public class CUserManagementRepository : ICUserManagementRepository
    {
        private readonly HotelManagementContext _context;
        private IMapper _mapper;
        public CUserManagementRepository(HotelManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> CreateUser(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);

                TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                user.CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istTimeZone);
                user.CreatedBy = user.FirstName;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return "User Created Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public async Task<string> DeleteUser(UserDTO deleteUserDTO)
        {
            try
            {
                var user = await _context.Users.FindAsync(deleteUserDTO.UserId);
                if (user != null)
                {
                    user.IsActive = false;
                    user.ModifiedBy = deleteUserDTO.ModifiedBy;

                    TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    user.ModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istTimeZone);

                    await _context.SaveChangesAsync();
                }
                return "User Deleted Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public async Task<string> UpdateUser(UserDTO updateUserDTO)
        {

            try
            {
                var user = _mapper.Map<User>(updateUserDTO);
                var existingUsers = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);

                if (existingUsers != null)
                {
                    existingUsers.FirstName = user.FirstName ?? existingUsers.FirstName;
                    existingUsers.LastName = user.LastName ?? existingUsers.LastName;
                    existingUsers.UserEmail = user.UserEmail ?? existingUsers.UserEmail;
                    existingUsers.UserPassword = user.UserPassword ?? existingUsers.UserPassword;
                    existingUsers.UserPhoneNumber = user.UserPhoneNumber ?? existingUsers.UserPhoneNumber;
                    existingUsers.UserRole = user.UserRole ?? existingUsers.UserRole;
                    existingUsers.ModifiedBy = user.ModifiedBy;

                    TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    existingUsers.ModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istTimeZone);

                    await _context.SaveChangesAsync();
                    return "Updated Successfully";
                }
                throw new NullReferenceException("No user found");
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }
    }
}
