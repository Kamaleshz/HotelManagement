using Feedback.Interface.RepositoryInterface.QueryInterface;
using Feedback.Models;
using Feedback.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Repository
{
    public class QFeedbackRepository : IQFeedbackRepository
    {
        private readonly FeedBackContext _context;

        public QFeedbackRepository(FeedBackContext context)
        {
            _context = context;
        }

        public async Task<ICollection<FeedBack>> Get()
        {
            try
            {
                var feedbacks = await _context.FeedBacks.Where(f => f.IsActive == true).ToListAsync();
                if (feedbacks == null)
                    throw new NullReferenceException("No feedbacks found");
                return feedbacks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomId(int roomId)
        {
            return await _context.GetFeedbacksByRoomId(roomId);
        }

        public async Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomTypeId(int roomTypeId)
        {
            return await _context.GetFeedbacksByRoomTypeId(roomTypeId);
        }
    }
}
