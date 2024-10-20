using EventManagement_Backend.Models;
using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using EventManagement_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Frontend.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet]
   
        // GET: Booking/Index
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookings();
            ViewBag.userId = HttpContext.Session.GetString("UserId");
            return View(bookings);
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingDetails(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        public async Task<IActionResult> Book()
        {

            BookingModel bookingModel = new BookingModel();
            var result = await _bookingService.AddBook(bookingModel);
            if (result)
            {
                return RedirectToAction(nameof(Index)); // Redirect to Index on success
            }
            ModelState.AddModelError("", "Error creating booking. Please try again."); // Show error
            return View();
        }
            

    }
}


