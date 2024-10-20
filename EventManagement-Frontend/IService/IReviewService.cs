using EventManagement_Frontend.Models;

namespace EventManagement_Frontend.IService
{
    public interface IReviewService
    {
        Task<List<ReviewModel>> GetAllReviews();
    }
}
