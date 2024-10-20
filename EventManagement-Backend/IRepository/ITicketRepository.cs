using EventManagement_Backend.Models;

namespace EventManagement_Backend.IRepository
{
    public interface ITicketRepository
    {
        Ticket BookTicket(Ticket ticket);

        bool CancelTicket(int ticketId);

        IEnumerable<Ticket> GetBookedTickets(int TicketId);
    }
}
