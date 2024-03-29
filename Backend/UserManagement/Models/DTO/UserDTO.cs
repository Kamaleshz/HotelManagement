namespace UserManagement.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserEmail { get; set; }

        public string? Password { get; set; }
        public byte[]? UserPassword { get; set; }

        public byte[]? HashKey { get; set; }

        public long? UserPhoneNumber { get; set; }

        public int? UserRole { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string? Token { get; set; }
    }
}
