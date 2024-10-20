using AutoMapper;
using EventManagement_Backend.Models;
using EventManagement_Backend.PaymentDTOs;

namespace EventManagement_Backend.MappingProfile
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>().ReverseMap();
        }
    }
}
