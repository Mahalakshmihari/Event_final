using EventManagement_Backend.CustomException;
using EventManagement_Backend.IRepository;
using EventManagement_Backend.PaymentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Creates a new booking.
        /// Throws ArgumentNullException if bookingDTO is null.
        /// Catches exceptions and returns appropriate HTTP responses.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BookingDTO>> CreateBooking([FromBody] BookingDTO bookingDTO)
        {
            try
            {
                if (bookingDTO == null)
                {
                    // Throw ArgumentNullException if booking data is invalid
                    throw new ArgumentNullException(nameof(bookingDTO), "Invalid booking data.");
                }

                var createdBooking = await _bookingService.CreateBookingAsync(bookingDTO);
                return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.BookingId }, createdBooking);
            }
            catch (ArgumentNullException ex)
            {
                // Handle invalid data scenario
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a booking by its ID.
        /// Throws BookingNotFoundException if the booking is not found.
        /// Catches exceptions and returns appropriate HTTP responses.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int id)
        {
            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id)
   ;
                if (booking == null)
                {
                    // Throw custom exception if booking not found
                    throw new BookingNotFoundException($"Booking with ID {id} was not found.");
                }

                return Ok(booking);
            }
            catch (BookingNotFoundException ex)
            {
                // Handle booking not found scenario
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all bookings.
        /// Catches exceptions and returns appropriate HTTP responses.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookingsAsync();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a booking by its ID.
        /// Throws BookingNotFoundException if the booking is not found or cannot be deleted.
        /// Catches exceptions and returns appropriate HTTP responses.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            try
            {
                var isDeleted = await _bookingService.DeleteBookingAsync(id)
   ;
                if (!isDeleted)
                {
                    // Throw custom exception if booking not found or could not be deleted
                    throw new BookingNotFoundException($"Booking with ID {id} was not found");
                }

                return Ok();
            }
            catch (BookingNotFoundException ex)
            {
                // Handle booking not found or deletion error
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
