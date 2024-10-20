using AutoMapper;
using EventManagement_Backend.PaymentDTOs;
using EventManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement_Backend.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly EventManagementDbContext _context;

        public BookingRepository(EventManagementDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new booking and saves it to the database.
        /// </summary>
        /// <param name="booking">The booking object to be created.</param>
        /// <returns>The created booking object.</returns>
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        /// <summary>
        /// Retrieves a booking by its ID.
        /// Returns null if the booking is not found.
        /// </summary>
        /// <param name="id">The ID of the booking to retrieve.</param>
        /// <returns>The booking object if found; otherwise, null.</returns>
        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all bookings from the database.
        /// </summary>
        /// <returns>A list of all booking objects.</returns>
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        /// <summary>
        /// Deletes a booking by its ID.
        /// Returns true if the booking was deleted; otherwise, false.
        /// </summary>
        /// <param name="id">The ID of the booking to delete.</param>
        /// <returns>A boolean indicating the success of the deletion.</returns>
        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await GetBookingByIdAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }



}
