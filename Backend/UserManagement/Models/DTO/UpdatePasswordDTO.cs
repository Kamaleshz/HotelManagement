namespace UserManagement.Models.DTO
{
    public class UpdatePasswordDTO
    {
        public int UserId { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
