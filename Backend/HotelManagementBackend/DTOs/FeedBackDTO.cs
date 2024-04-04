namespace HotelManagementBackend.DTOs
{
    public class FeedBackDTO 
    {
        public int FeedBackID { get; set; }
        public string? Review {  get; set; } 
        public int rating { get; set; }
        public int BookingID { get; set; }

        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set;}
    }
}
