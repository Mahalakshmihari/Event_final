
using EventManagement_Backend.Models;

namespace EventManagement_Backend.Repository
{
    public interface IBookingRepository
    {

        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<bool> DeleteBookingAsync(int id);
    }

}