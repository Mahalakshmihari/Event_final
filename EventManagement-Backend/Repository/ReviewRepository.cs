using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement_Backend.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly EventManagementDbContext _Context;
        
        public ReviewRepository(EventManagementDbContext context)
        {
            _Context = context;
          
        }

        public string AddReview(Review review)
        {
            if (review != null)
            {

                _Context.Reviews.Add(review);
                _Context.SaveChanges();

                return "Review Added Successfully";
            }
            else
            {
                return "Error Adding review";
            }

        }

        public string DeleteReview(int reviewId)
        {
            if (reviewId != 0)
            {
                var ChechId = _Context.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
                if (ChechId != null)
                {
                    _Context.Reviews.Remove(ChechId);
                    _Context.SaveChanges();
                    return "Review Deleted successfully";
                }
                else
                {
                    return "Error deleting Review";
                }
            }
            else
            {
                return "EventId not found";
            }
        }

        public List<Review> GetAllReviews()
        {
            return _Context.Reviews.ToList();
        }

        public Review GetReviewsByEventId(int eventId)
        {
            Review eventReviews = _Context.Reviews.Where(r => r.EventId == eventId).FirstOrDefault();
            if (eventReviews != null)
            {
                return eventReviews;
            }
            else
            {
                return null;
            }
        }

        public string UpdateReview(Review updatereview)
        {
            if (updatereview != null)
            {
                var review = _Context.Reviews.FirstOrDefault(r => r.ReviewId == updatereview.ReviewId);
                if (review != null)
                {
                    review.Rating = updatereview.Rating;
                    review.Comment = updatereview.Comment;
                    review.ReviewDate = updatereview.ReviewDate;
                    _Context.SaveChanges();

                    return "Updated Successfully";
                }
                else
                {
                    return "Error Updating Review";
                }
            }
            else
            {
                return "Review Id not found";
            }

        }
    }
}
