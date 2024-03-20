using Microsoft.EntityFrameworkCore;
using UserManagement.Interface.RepositoryInterface.CommandInterface;
using UserManagement.Models;

namespace UserManagement.Repository.CommandRepository
{
    public class CUserManagementRepository : ICUserManagementRepository
    {
        private readonly UserManagementContext _context;

        public CUserManagementRepository(UserManagementContext context)
        {
            _context = context;
        }
        public async Task<string> CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return "User Created Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" +  ex.Message);
            }
        }

        public async Task<string> DeleteUser(int UserId)
        {
            try
            {
                var user = await _context.Users.FindAsync(UserId);
                if (user != null)
                {
                    user.IsActive = false;
                    await _context.SaveChangesAsync();
                }
                return "User Deleted Successfully";
            }
            catch(Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public async Task<string> UpdateUser(User user)
        {
            try
            {
                var existingUsers = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);

                if (existingUsers != null)
                {
                    existingUsers.FirstName = user.FirstName ?? existingUsers.FirstName;
                    existingUsers.LastName = user.LastName ?? existingUsers.LastName;
                    existingUsers.UserEmail = user.UserEmail ?? existingUsers.UserEmail;
                    existingUsers.UserPassword = user.UserPassword ?? existingUsers.UserPassword;
                    existingUsers.UserPhoneNumber = user.UserPhoneNumber ?? existingUsers.UserPhoneNumber;
                    existingUsers.UserRole = user.UserRole ?? existingUsers.UserRole;
                    existingUsers.ModifiedBy = user.ModifiedBy ?? existingUsers.ModifiedBy;

                    TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    existingUsers.ModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istTimeZone);

                    await _context.SaveChangesAsync();
                    return "Updated Successfully";
                }
                throw new NullReferenceException("No user found");
            }
            catch(Exception ex)
            {
                throw new Exception("Error:" +  ex.Message);
            }
        }
    }
}
