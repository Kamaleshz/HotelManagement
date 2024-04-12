using Feedback.Models.DTO_s;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Models
{
    public partial class FeedBackContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeedbackDTO>().HasNoKey();
        }

        public async Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomId(int roomId)
        {
            try
            {
                string sql = "EXEC GetFeedbackByRoomId @RoomId";
                SqlParameter roomIdParameter = new SqlParameter("@RoomId", roomId);

                var feedbacks = await Set<FeedbackDTO>().FromSqlRaw(sql, roomIdParameter).AsQueryable().ToListAsync();

                if (feedbacks.Count == 0)
                    throw new NullReferenceException("No feedback found");
                return feedbacks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomTypeId(int roomTypeId)
        {
            try
            {
                string sql = "EXEC GetFeedbacksByRoomTypeId @RoomTypeId";
                SqlParameter roomTypeIdParameter = new SqlParameter("@RoomTypeId", roomTypeId);

                var feedbacks = await Set<FeedbackDTO>().FromSqlRaw(sql, roomTypeIdParameter).AsQueryable().ToListAsync();

                if (feedbacks.Count == 0)
                    throw new NullReferenceException("No feedbacks found");
                return feedbacks;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
