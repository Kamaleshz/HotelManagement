namespace UserManagement.Models.DTO
{
    public class UpdatePasswordDTO
    {
        public int Id { get; set; }
        public string? Password { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
