using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;

namespace EventManagement_Backend.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private readonly EventManagementDbContext _context;
        public SeatRepository(EventManagementDbContext context)
        {
            _context = context;
        }

        public bool UpdateSeat(int seatId, List<int> seatIds)
        {
            var seats = _context.Seats.Where(s => seatIds.Contains(s.SeatId)).ToList();
            if (seats != null && seats.Any())
            {
                foreach (var seat in seats)
                {
                    if (seat.IsAvailble == true) // Ensure the seat isn't already booked
                    {
                        seat.IsAvailble = false;

                        // Add a new booking record
                        //_context.Bookings.Add(new Booking
                        //{
                        //    UserId = userId,
                        //    EventId = seat.EventId,
                        //    SeatId = seat.SeatId,
                        //    BookingDate = DateTime.Now
                        //});

                        // Mark the seat as modified
                        _context.Entry(seat).Property(p => p.IsAvailble).IsModified = true;
                    }
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

