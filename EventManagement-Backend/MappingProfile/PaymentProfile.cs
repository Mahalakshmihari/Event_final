using AutoMapper;
using EventManagement_Backend.PaymentDTOs;
using EventManagement_Backend.Models;

namespace EventManagement_Backend.MappingProfile
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDTO>().ReverseMap();
        }
    }
}
