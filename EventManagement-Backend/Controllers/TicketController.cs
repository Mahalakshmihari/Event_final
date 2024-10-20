using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        [HttpPost]
        public IActionResult BookTicket([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest("Invalid ticket information.");
            }

            // Book the ticket
            var bookedTicket = _ticketRepository.BookTicket(ticket);
            return Ok(new { message = "Ticket booked successfully.", TicketId = bookedTicket.TicketId });
        }

        // DELETE: api/ticket/cancel/{ticketId}
        [HttpDelete("cancel/{ticketId}")]
        public IActionResult CancelTicket(int ticketId)
        {
            // Cancel the ticket
            var isCancelled = _ticketRepository.CancelTicket(ticketId);

            if (!isCancelled)
            {
                return NotFound(new { message = "Ticket not found or cancellation failed." });
            }

            return Ok(new { message = "Ticket canceled successfully." });
        }

        // GET: api/ticket/bookings/{bookingId}
        [HttpGet("bookings/{bookingId}")]
        public IActionResult GetBookedTickets(int bookingId)
        {
            var tickets = _ticketRepository.GetBookedTickets(bookingId);

            if (tickets == null || !tickets.Any())
            {
                return NotFound(new { message = "No tickets found for this booking." });
            }

            return Ok(tickets);
        }
    }
}
