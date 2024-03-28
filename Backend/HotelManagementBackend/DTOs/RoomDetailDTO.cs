using System.Data.SqlTypes;

namespace HotelManagementBackend.DTOs
{
    public class RoomDetailDTO
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string? RoomTypeName { get; set; }
        public string? RoomTypeDescription { get; set; }
        public int Price { get; set; }
        public string? AmenityNames { get; set; }
    }
}
