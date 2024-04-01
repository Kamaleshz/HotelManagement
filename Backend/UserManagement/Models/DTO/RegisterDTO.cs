using System.Numerics;

namespace UserManagement.Models.DTO
{
    public class RegisterDTO : User
    {
        public new string? Password { get; set; }
    }
}
