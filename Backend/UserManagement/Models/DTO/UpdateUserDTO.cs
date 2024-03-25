using System.Numerics;

namespace UserManagement.Models.DTO
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public BigInteger PhoneNumber { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
