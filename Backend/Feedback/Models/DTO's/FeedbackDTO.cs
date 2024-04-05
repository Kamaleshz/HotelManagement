namespace Feedback.Models.DTO_s
{
    public class FeedbackDTO
    {
        public int FeedbackId { get; set; }
        public int RoomId {  get; set; }
        public string? Review {  get; set; }
        public int Rating { get; set; }

    }
}
