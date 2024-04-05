using Feedback.Models;

namespace Feedback.Interface.RepositoryInterface.QueryInterface
{
    public interface IQFeedbackRepository
    {
        public Task<ICollection<FeedBack>> Get();

        public Task<ICollection<FeedBack>> GetById(int id);
    }
}
