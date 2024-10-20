
using EventManagement_Backend.Models;
using EventManagement_Backend.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EventManagement_Backend_Service_TestProject
{
    [TestFixture]
    public class TicketRepositoryTests
    {
        private Mock<DbSet<Ticket>> _mockSet;
        private Mock<EventManagementDbContext> _mockContext;
        private TicketRepository _repository;

        [SetUp]
        public void Setup()
        {
            var tickets = new List<Ticket>
            {
                new Ticket { TicketId = 1, SeatId = 101, BookingId = 201, NumberOfTickets = 2, TotalAmount = 300 },
                new Ticket { TicketId = 2, SeatId = 102, BookingId = 202, NumberOfTickets = 3, TotalAmount = 450 }
            }.AsQueryable();

            _mockSet = CreateDbSetMock(tickets);
            _mockContext = new Mock<EventManagementDbContext>();
            _mockContext.Setup(m => m.Tickets).Returns(_mockSet.Object);
            _repository = new TicketRepository(_mockContext.Object);
        }

        public static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> elements) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elements.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elements.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elements.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elements.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => elements.FirstOrDefault(d => (int)d.GetType().GetProperty("TicketId").GetValue(d, null) == (int)ids[0]));
            return dbSetMock;
        }

        [Test]
        public void BookTicket_ShouldAddTicket_WhenTicketIsValid()
        {
            var newTicket = new Ticket { TicketId = 3, SeatId = 103, BookingId = 203, NumberOfTickets = 1, TotalAmount = 150 };

            var result = _repository.BookTicket(newTicket);

            _mockSet.Verify(m => m.Add(It.IsAny<Ticket>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo(newTicket));
        }

        [Test]
        public void CancelTicket_ShouldRemoveTicket_WhenTicketExists()
        {
            var result = _repository.CancelTicket(1);

            _mockSet.Verify(m => m.Remove(It.IsAny<Ticket>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.True);
        }

        [Test]
        public void CancelTicket_ShouldReturnFalse_WhenTicketDoesNotExist()
        {
            var result = _repository.CancelTicket(99);

            _mockSet.Verify(m => m.Remove(It.IsAny<Ticket>()), Times.Never());
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetBookedTickets_ShouldReturnTickets_WhenBookingIdExists()
        {
            var result = _repository.GetBookedTickets(201);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().TicketId, Is.EqualTo(1));
        }

        [Test]
        public void GetBookedTickets_ShouldReturnEmpty_WhenBookingIdDoesNotExist()
        {
            var result = _repository.GetBookedTickets(999);

            Assert.That(result, Is.Empty);
        }
    }
}












