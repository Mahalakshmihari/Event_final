
using EventManagement_Backend.Models;
using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventManagement_Frontend.Controllers
{
    public class SeatController : Controller
    {
        private readonly EventManagementDbContext _context;
        public SeatController(EventManagementDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int eventId = 3)
        {
            TempData.Keep();
            var seats = _context.Seats
                .Where(s => s.EventId == eventId)
                .Select(seat => new SeatModel
                {
                    SeatId = seat.SeatId,
                    SeatNumber = seat.SeatNumber,
                    IsAvailble = seat.IsAvailble,
                    EventId = seat.EventId
                }).ToList();

            return View(seats);
        }

        [HttpPost]
        public IActionResult BookSeats(int userId, List<int> seatIds)
        {
            // Fetch the selected seats based on seat IDs
            var seats = _context.Seats.Where(s => seatIds.Contains(s.SeatId)).ToList();

            // Check if seats exist and are valid
            if (seats == null || !seats.Any())
            {
                return Json(new { success = false, message = "No valid seats found." });
            }

            // Validate if any of the selected seats are already booked (1 = booked, 0 = available)
            foreach (var seat in seats)
            {
                // Check if the seat is booked (IsAvailable = 1)
                if (seat.IsAvailble == true) // 1 means booked
                {
                    return Json(new { success = false, message = $"Seat {seat.SeatNumber} is already booked." });
                }
            }

            // Proceed with booking all the valid seats
            foreach (var seat in seats)
            {
                seat.IsAvailble = true; // Mark seat as booked (1 = booked)

                _context.Entry(seat).Property(p => p.IsAvailble).IsModified = true;
            }

            // Save changes to the database
            _context.SaveChanges();

            // Return success response
            return Json(new
            {
                success = true
               ,
                message = "Seats Selection success ."
            });
        }
        [HttpGet]
        public IActionResult CalculateAmount(int seats)
        {
            // Assume each seat costs 100
            var seatCost = 100;
            var totalAmount = seats * seatCost;

            // Create the PaymentResultViewModel and populate it
            var model = new PaymentResultViewModel
            {
                TotalAmount = totalAmount
            };
            TempData["totalAmount"] = totalAmount;

            // Return the view with the model
            return View("DisplayTotalAmount", model);
        }

    }
}
