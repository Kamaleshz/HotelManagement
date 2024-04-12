using AutoMapper;
using Feedback.Models;
using Feedback.Models.DTO_s;

namespace Feedback.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<FeedbackDTO, FeedBack>().ReverseMap();
        }
    }
}
