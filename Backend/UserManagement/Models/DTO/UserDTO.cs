namespace UserManagement.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserEmail { get; set; }

        public string? UserPassword { get; set; }

        public long? UserPhoneNumber { get; set; }

        public int? UserRole { get; set; }

        public string? CreatedBy { get; set; }
    }
}
