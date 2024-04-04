namespace HotelManagementBackend.DTOs
{
    public class RoomFilterDTO
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string? AmenityIds { get; set; }
    }
}
