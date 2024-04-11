using Feedback.Models;
using Feedback.Models.DTO_s;

namespace Feedback.Interface.RepositoryInterface.QueryInterface
{
    public interface IQFeedbackRepository
    {
        public Task<ICollection<FeedBack>> Get();

        public Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomId(int roomId);

        public Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomTypeId(int roomTypeId);
    }
}
