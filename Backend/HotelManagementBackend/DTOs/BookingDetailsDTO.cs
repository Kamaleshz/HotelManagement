namespace HotelManagementBackend.DTOs
{
    public class BookingDetailsDTO
    {
        public int BookingID {  get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public decimal Amount { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int PeopleCount { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
