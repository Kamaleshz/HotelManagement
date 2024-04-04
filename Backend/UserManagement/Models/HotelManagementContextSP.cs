using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models.DTO;

namespace UserManagement.Models
{
    public partial class HotelManagementContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDTO>().HasNoKey();
        }

        public async Task<string> GetRoleNameById(UserDTO userDTO)
        {
            try
            {
                string sql = "EXEC GetRoleNameById @UserId";
                SqlParameter userIdParameter = new SqlParameter("@UserId", userDTO.UserId);

                var roleName = await Database.ExecuteSqlRawAsync(sql, userIdParameter);

                if (roleName.ToString() == "")
                    throw new NullReferenceException("No Role found for this user ID.");

                return roleName.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }


    }
}
