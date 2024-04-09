namespace HotelManagementBackend.ViewModels
{
    public class RoomTypeView
    {
        public int RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
        public string? RoomTypeDescription { get; set; }
        public decimal Price { get; set; }
        public string? AmenityNames { get; set; }
    }
}
