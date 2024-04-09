using Feedback.Models;
using Feedback.Models.DTO_s;

namespace Feedback.Interface.ServiceInterface
{
    public interface IFeedbackService
    {
        public Task<string> CreateFeedback(FeedbackDTO feedback);

        public Task<string> UpdateFeedback(FeedbackDTO feedback);

        public Task<string> DeleteFeedback(FeedbackDTO feedback);

        public Task<ICollection<FeedbackDTO>> GetAllFeedbacks();

        public Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomId(FeedbackDTO feedbackDTO);
    }
}
