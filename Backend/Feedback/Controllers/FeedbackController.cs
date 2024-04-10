using Feedback.Interface.ServiceInterface;
using Feedback.Models.DTO_s;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback(FeedbackDTO feedbackDTO)
        {
            try
            {
                var result = await _feedbackService.CreateFeedback(feedbackDTO);
                return Ok(new Response<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(new string(""),ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeedback(FeedbackDTO feedbackDTO)
        {
            try
            {
                var result = await _feedbackService.UpdateFeedback(feedbackDTO);
                return Ok(new Response<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(new string(""), ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeedback(FeedbackDTO feedbackDTO)
        {
            try
            {
                var result = await _feedbackService.DeleteFeedback(feedbackDTO);
                return Ok(new Response<string>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new Response<string>(new string(""),ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFeebackByRoomId(int feedbackDTO)
        {
            try
            {
                var result = await _feedbackService.GetFeedbacksByRoomId(feedbackDTO);
                return Ok(new Response<ICollection<FeedbackDTO>>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new Response<FeedbackDTO>(new FeedbackDTO(), ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                var result = await _feedbackService.GetAllFeedbacks();
                return Ok(new Response<ICollection<FeedbackDTO>>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new Response<FeedbackDTO>(new FeedbackDTO(), ex.Message));
            }
        }
    }
}
