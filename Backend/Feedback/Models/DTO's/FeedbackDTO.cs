namespace Feedback.Models.DTO_s
{
    public class FeedbackDTO
    {
        public int FeedbackId { get; set; }
        public int? RoomId {  get; set; }
        public string? Review {  get; set; }
        public int Rating { get; set; }
        public bool? IsActive {  get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
