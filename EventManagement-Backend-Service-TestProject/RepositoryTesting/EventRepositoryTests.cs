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
    public class EventRepositoryTests
    {
        private Mock<DbSet<Event>> _mockSet;
        private Mock<EventManagementDbContext> _mockContext;
        private EventRepository _repository;

        [SetUp]
        public void Setup()
        {
            var events = new List<Event>
            {
                new Event { EventId = 1, EventName = "Event 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Location = "Location 1", Price = 100, ImageUrl = "image1.jpg", Rating = 4, TotalTickets = 100, AvailableTickets = 50 },
                new Event { EventId = 2, EventName = "Event 2", Description = "Description 2", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Location = "Location 2", Price = 200, ImageUrl = "image2.jpg", Rating = 5, TotalTickets = 200, AvailableTickets = 100 }
            }.AsQueryable();

            _mockSet = CreateDbSetMock(events);
            _mockContext = new Mock<EventManagementDbContext>();
            _mockContext.Setup(m => m.Events).Returns(_mockSet.Object);
            _repository = new EventRepository(_mockContext.Object);
        }

        public static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> elements) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elements.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elements.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elements.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elements.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => elements.FirstOrDefault(d => (int)d.GetType().GetProperty("EventId").GetValue(d, null) == (int)ids[0]));
            return dbSetMock;
        }

        [Test]
        public void GetAllEvents_ShouldReturnAllEvents()
        {
            var result = _repository.GetAllEvents();
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetEventById_ShouldReturnEvent_WhenEventExists()
        {
            var result = _repository.GetEventById(1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.EventName, Is.EqualTo("Event 1"));
        }

        [Test]
        public void GetEventById_ShouldReturnNull_WhenEventDoesNotExist()
        {
            var result = _repository.GetEventById(99);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void CreateEvent_ShouldAddEvent_WhenEventIsValid()
        {
            var newEvent = new Event { EventId = 3, EventName = "Event 3", Description = "Description 3", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Location = "Location 3", Price = 300, ImageUrl = "image3.jpg", Rating = 3, TotalTickets = 300, AvailableTickets = 150 };

            var result = _repository.CreateEvent(newEvent);

            _mockSet.Verify(m => m.Add(It.IsAny<Event>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("added successfully"));
        }

        [Test]
        public void UpdateEvent_ShouldUpdateEvent_WhenEventExists()
        {
            var updatedEvent = new Event { EventId = 1, EventName = "Updated Event", Description = "Updated Description", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Location = "Updated Location", Price = 150, ImageUrl = "updatedimage.jpg", Rating = 4, TotalTickets = 150, AvailableTickets = 75 };

            var result = _repository.UpdateEvent(updatedEvent);

            //_mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("updated successfully"));
        }

        [Test]
        public void UpdateEvent_ShouldReturnError_WhenEventDoesNotExist()
        {
            var updatedEvent = new Event { EventId = 99, EventName = "Nonexistent Event", Description = "Nonexistent Description", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Location = "Nonexistent Location", Price = 150, ImageUrl = "nonexistentimage.jpg", Rating = 4, TotalTickets = 150, AvailableTickets = 75 };

            var result = _repository.UpdateEvent(updatedEvent);

            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("not updated"));
        }

        [Test]
        public void DeleteEvent_ShouldRemoveEvent_WhenEventExists()
        {
            var result = _repository.DeleteEvent(1);

            _mockSet.Verify(m => m.Remove(It.IsAny<Event>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("removed successfully"));
        }

        [Test]
        public void DeleteEvent_ShouldReturnError_WhenEventDoesNotExist()
        {
            var result = _repository.DeleteEvent(99);

            _mockSet.Verify(m => m.Remove(It.IsAny<Event>()), Times.Never());
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("not removed"));
        }

        [Test]
        public void GetByName_ShouldReturnEvent_WhenEventNameExists()
        {
            var result = _repository.GetByName("Event 1");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.EventName, Is.EqualTo("Event 1"));
        }

        [Test]
        public void GetByName_ShouldReturnNull_WhenEventNameDoesNotExist()
        {
            var result = _repository.GetByName("Nonexistent Event");
            Assert.That(result, Is.Null);
        }
    }
}