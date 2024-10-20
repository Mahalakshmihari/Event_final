using EventManagement_Backend.Models;

namespace EventManagement_Backend.IRepository
{
    public interface IReviewRepository
    {
        string AddReview(Review review);
        string DeleteReview(int reviewId);
        string UpdateReview(Review review);
        List<Review> GetAllReviews();
        Review GetReviewsByEventId(int eventId);
    }
}
