using AutoMapper;
using EventManagement_Backend.PaymentDTOs;
using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;
using EventManagement_Backend.Repository;

namespace EventManagement_Backend.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new booking from the provided BookingDTO and saves it.
        /// </summary>
        /// <param name="bookingDTO">The BookingDTO object to be created.</param>
        /// <returns>The created BookingDTO object.</returns>
        public async Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDTO)
        {
            var booking = _mapper.Map<Booking>(bookingDTO);
            var createdBooking = await _bookingRepository.CreateBookingAsync(booking);
            return _mapper.Map<BookingDTO>(createdBooking);
        }

        /// <summary>
        /// Retrieves a booking by its ID and maps it to a BookingDTO.
        /// Returns null if the booking is not found.
        /// </summary>
        /// <param name="id">The ID of the booking to retrieve.</param>
        /// <returns>The BookingDTO object if found; otherwise, null.</returns>
        public async Task<BookingDTO?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            return booking != null ? _mapper.Map<BookingDTO>(booking) : null;
        }

        /// <summary>
        /// Retrieves all bookings and maps them to a list of BookingDTOs.
        /// </summary>
        /// <returns>A list of all BookingDTO objects.</returns>
        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        }

        /// <summary>
        /// Deletes a booking by its ID.
        /// </summary>
        /// <param name="id">The ID of the booking to delete.</param>
        /// <returns>A boolean indicating the success of the deletion.</returns>
        public async Task<bool> DeleteBookingAsync(int id)
        {
            return await _bookingRepository.DeleteBookingAsync(id);
        }
    }


}
