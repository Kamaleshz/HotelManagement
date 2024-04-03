namespace HotelManagementBackend.ViewModels
{
    public class BookingView
    {
        public int BookingId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int RoomNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime Checkout { get; set; }
        public int PeopleCount { get; set; }
    }
}
