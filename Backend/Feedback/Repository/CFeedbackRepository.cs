using Microsoft.EntityFrameworkCore;
using Feedback.Interface.RepositoryInterface.CommandInterface;
using Feedback.Models;
using Feedback.Models.DTO_s;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using System;

namespace Feedback.Repository
{
    public class CFeedbackRepository : ICFeedbackRepository
    {
        private readonly FeedBackContext _context;

        public CFeedbackRepository(FeedBackContext context)
        {
            _context = context;
        }

        public async Task<string> Create(FeedBack feedBack)
        {
            try
            {
                TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                feedBack.CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istTimeZone);

                await _context.AddAsync(feedBack);
                await _context.SaveChangesAsync();

                return "Feedback Posted Successfully";
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<string> Delete(FeedBack feedBack)
        {
            try
            {
                var feedback = await _context.FeedBacks.FindAsync(feedBack.FeedBackId);
                if (feedback == null)
                    throw new NullReferenceException("Can't find this feedback");
                feedback.IsActive = false;
                TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                feedback.ModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istTimeZone);

                await _context.SaveChangesAsync();

                return "Deleted Successfully";
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<string> Update(FeedBack feedBack)
        {
            try
            {
                var existingFeedback = await _context.FeedBacks.FirstOrDefaultAsync(x => x.FeedBackId == feedBack.FeedBackId && x.IsActive == true);

                if (existingFeedback == null)
                    throw new NullReferenceException("No feedback found");
                existingFeedback.Review = feedBack.Review ?? existingFeedback.Review;
                existingFeedback.Rating = feedBack.Rating ?? existingFeedback.Rating;
                existingFeedback.CreatedBy = existingFeedback.CreatedBy;
                existingFeedback.CreatedOn = existingFeedback.CreatedOn;
                existingFeedback.ModifiedBy = feedBack.ModifiedBy ?? existingFeedback.ModifiedBy;
                TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                feedBack.ModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istTimeZone);

                await _context.SaveChangesAsync();
                return "Feedback Updated Successfully";
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
