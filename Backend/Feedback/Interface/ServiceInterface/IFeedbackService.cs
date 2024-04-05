using Feedback.Models;
using Feedback.Models.DTO_s;

namespace Feedback.Interface.ServiceInterface
{
    public interface IFeedbackService
    {
        public Task<string> CreateFeedback(FeedBack feedback);

        public Task<string> UpdateFeedback(FeedBack feedback);

        public Task<string> DeleteFeedback(FeedBack feedback);

        public Task<ICollection<FeedBack>> GetFeedbacksByRoomId(FeedbackDTO feedbackDTO);
    }
}
