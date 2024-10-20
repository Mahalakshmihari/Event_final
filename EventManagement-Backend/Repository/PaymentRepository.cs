using AutoMapper;
using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;
using EventManagement_Backend.PaymentDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EventManagement_Backend.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMapper _mapper;
        private readonly EventManagementDbContext _context;

        public PaymentRepository(EventManagementDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Add the Card Details, UserId, EventID.
        /// If the card details are not valid it return Payment processing failed.
        /// </summary>
        /// <param name="processPaymentDto"></param>
        /// <returns></returns>
        public PaymentDTO ProcessPayment(ProcessPaymentDTO processPaymentDto)
        {

            // Validate card details (basic validation)
            if (!IsValidCard(processPaymentDto.CardNumber, processPaymentDto.ExpirationDate, processPaymentDto.CVV))
            {
                return null;
            }

            var payment = new Payment
            {
                UserId = processPaymentDto.UserId,
                //UserId = user.Id, // Use the found UserId
                TotalAmount = processPaymentDto.TotalAmount,
                EventId = processPaymentDto.EventId,
                PaymentDate = DateTime.Now,
                Cvv = int.Parse(processPaymentDto.CVV),
                CardNumber = long.Parse(processPaymentDto.CardNumber),
                ExpiryDate = DateOnly.Parse(processPaymentDto.ExpirationDate),
                PaymentStatus = "confirmed"
            };
            _context.Payments.Add(payment);
            _context.SaveChanges();
            var paymentDto = _mapper.Map<PaymentDTO>(payment);
            return paymentDto;
        }

        /// <summary>
        /// Get a list of all processed payments.
        /// </summary>
        /// <returns>A list of PaymentDTOs</returns>
        public List<PaymentDTO> GetAllPayments()
        {
            var payments = _context.Payments.ToList();

            // Map the payments to a list of PaymentDTO
            var paymentDtos = _mapper.Map<List<PaymentDTO>>(payments);

            return paymentDtos;
        }

        public bool IsValidCard(string cardNumber, string expirationDate, string cvv)
        {
            bool isValidCardNumber = cardNumber.Length == 16;
            bool isValidExpirationDate = DateTime.TryParseExact(expirationDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var expiryDate) && expiryDate > DateTime.Now;
            bool isValidCvv = cvv.Length == 3;

            return isValidCardNumber && isValidExpirationDate && isValidCvv;
        }
    }
}
