using EventManagement_Backend.PaymentDTOs;

namespace EventManagement_Backend.IRepository
{
    public interface IBookingService
    {
        Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDTO);
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();
        Task<bool> DeleteBookingAsync(int id);
    }
}
