using AutoMapper;
using Feedback.Interface.RepositoryInterface.CommandInterface;
using Feedback.Interface.RepositoryInterface.QueryInterface;
using Feedback.Interface.ServiceInterface;
using Feedback.Models;
using Feedback.Models.DTO_s;
using Microsoft.Identity.Client;

namespace Feedback.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ICFeedbackRepository _commandRepository;
        private readonly IQFeedbackRepository _queryRepository;
        private readonly IMapper _mapper;

        public FeedbackService(ICFeedbackRepository commandrepository, IQFeedbackRepository queryrepository, IMapper mapper)
        {
            _commandRepository = commandrepository;
            _queryRepository = queryrepository;
            _mapper = mapper;
        }

        public async Task<string> CreateFeedback(FeedbackDTO feedbackDTO)
        {
            var feedback = _mapper.Map<FeedBack>(feedbackDTO);
            return await _commandRepository.Create(feedback);
        }

        public Task<string> DeleteFeedback(FeedbackDTO feedbackDTO)
        {
            var feedback = _mapper.Map<FeedBack>(feedbackDTO);
            return _commandRepository.Delete(feedback);
        }

        public async Task<ICollection<FeedbackDTO>> GetAllFeedbacks()
        {
            var feedbacks = await _queryRepository.Get();
            var feedbackDTO = _mapper.Map<ICollection<FeedbackDTO>>(feedbacks);
            return feedbackDTO;
        }

        public Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomId(int roomId)
        {
            return _queryRepository.GetFeedbacksByRoomId(roomId);
        }

        public async Task<ICollection<FeedbackDTO>> GetFeedbacksByRoomTypeId(int roomTypeId)
        {
            return await _queryRepository.GetFeedbacksByRoomTypeId(roomTypeId);
        }

        public Task<string> UpdateFeedback(FeedbackDTO feedbackDTO)
        {
            var feedback = _mapper.Map<FeedBack>(feedbackDTO);
            return _commandRepository.Update(feedback);
        }
    }
}
