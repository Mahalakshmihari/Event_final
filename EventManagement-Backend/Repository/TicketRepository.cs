using EventManagement_Backend.Authentication;
using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;

namespace EventManagement_Backend.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly EventManagementDbContext _context;

        public TicketRepository(EventManagementDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Books a new ticket by adding it to the database and saving the changes.
        /// </summary>
        /// <param name="ticket">The ticket to be booked.</param>
        /// <returns>The booked ticket.</returns>
        public Ticket BookTicket(Ticket ticket)
        {

            _context.Tickets.Add(ticket);
            _context.SaveChanges(); 
            return ticket;
        }
        /// <summary>
        /// Cancels a ticket by removing it from the database.
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to be canceled.</param>
        /// <returns>True if the ticket was successfully canceled; false if no ticket with the specified ID exists.</returns>
        public bool CancelTicket(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket == null)
            {
                return false;
            }
            _context.Tickets.Remove(ticket);
            _context.SaveChanges(); 
            return true;
        }
        /// <summary>
        /// Retrieves all tickets that have been booked with the specified booking ID.
        /// </summary>
        /// <param name="TicketId">The ID of the booking.</param>
        /// <returns>A collection of tickets that match the booking ID.</returns>
        public IEnumerable<Ticket> GetBookedTickets(int TicketId)
        {
            return _context.Tickets.Where(t => t.BookingId == TicketId).ToList(); 
        }
    }
}
