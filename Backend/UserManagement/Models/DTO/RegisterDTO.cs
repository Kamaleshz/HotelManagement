using System.Numerics;

namespace UserManagement.Models.DTO
{
    public class RegisterDTO : User
    {
        public  string? Password { get; set; }
    }
}
