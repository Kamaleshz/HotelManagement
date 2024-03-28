using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models.DTO;

namespace UserManagement.Models
{
    public partial class HotelManagementContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleDTO>().HasNoKey();
        }

        public async Task<string> GetRoleNameById(UserDTO userDTO)
        {
            try
            {
                string sql = "EXEC GetRoleNameById @RoleId";
                SqlParameter roleIdParameter = new SqlParameter("@RoleId", userDTO.UserRole);

                var roleName = await Database.ExecuteSqlRawAsync(sql, roleIdParameter);

                if (roleName <= 0)
                    throw new NullReferenceException("No Role found under this ID:");
                return roleName.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

    }
}
