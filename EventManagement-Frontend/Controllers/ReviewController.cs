using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Frontend.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // Action to display all reviews
        public async Task<IActionResult> Index()
        {
            List<ReviewModel> reviews = await _reviewService.GetAllReviews();
            return View(reviews);
        }
    }
}