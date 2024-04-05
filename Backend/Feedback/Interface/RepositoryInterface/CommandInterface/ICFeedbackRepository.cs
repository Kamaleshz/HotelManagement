using Feedback.Models;

namespace Feedback.Interface.RepositoryInterface.CommandInterface
{
    public interface ICFeedbackRepository
    {
        public Task<string> Create(FeedBack feedBack);

        public Task<string> Update(FeedBack feedBack);

        public Task<string> Delete(FeedBack feedBack);
    }
}
