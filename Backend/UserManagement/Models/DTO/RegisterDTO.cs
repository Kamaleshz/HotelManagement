using System.Numerics;

namespace UserManagement.Models.DTO
{
    public class RegisterDTO : UserDTO
    {
        public string? Password { get; set; }
    }
}
