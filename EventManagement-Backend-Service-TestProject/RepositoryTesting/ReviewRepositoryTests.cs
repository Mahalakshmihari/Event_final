using EventManagement_Backend.Models;
using EventManagement_Backend.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Backend_Service_TestProject
{
    [TestFixture]
    public class ReviewRepositoryTests
    {
        private Mock<DbSet<Review>> _mockSet;
        private Mock<EventManagementDbContext> _mockContext;
        private ReviewRepository _repository;

        [SetUp]
        public void Setup()
        {
            var reviews = new List<Review>
            {
                new Review { ReviewId = 1, EventId = 101, Rating = 5, Comment = "Great event!", ReviewDate = DateTime.Now },
                new Review { ReviewId = 2, EventId = 102, Rating = 4, Comment = "Good event", ReviewDate = DateTime.Now }
            }.AsQueryable();

            _mockSet = CreateDbSetMock(reviews);
            _mockContext = new Mock<EventManagementDbContext>();
            _mockContext.Setup(m => m.Reviews).Returns(_mockSet.Object);
            _repository = new ReviewRepository(_mockContext.Object);
        }

        public static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> elements) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elements.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elements.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elements.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elements.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => elements.FirstOrDefault(d => (int)d.GetType().GetProperty("ReviewId").GetValue(d, null) == (int)ids[0]));
            return dbSetMock;
        }

        [Test]
        public void AddReview_ShouldAddReview_WhenReviewIsValid()
        {
            var review = new Review { ReviewId = 3, EventId = 103, Rating = 3, Comment = "Average event", ReviewDate = DateTime.Now };

            var result = _repository.AddReview(review);

            _mockSet.Verify(m => m.Add(It.IsAny<Review>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("Review Added Successfully"));
        }

        [Test]
        public void AddReview_ShouldReturnError_WhenReviewIsNull()
        {
            Review review = null;

            var result = _repository.AddReview(review);

            _mockSet.Verify(m => m.Add(It.IsAny<Review>()), Times.Never());
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("Error Adding review"));
        }

        [Test]
        public void DeleteReview_ShouldDeleteReview_WhenReviewExists()
        {
            var result = _repository.DeleteReview(1);

            _mockSet.Verify(m => m.Remove(It.IsAny<Review>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("Review Deleted successfully"));
        }

        [Test]
        public void DeleteReview_ShouldReturnError_WhenReviewDoesNotExist()
        {
            var result = _repository.DeleteReview(99);

            _mockSet.Verify(m => m.Remove(It.IsAny<Review>()), Times.Never());
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("Error deleting Review"));
        }

        [Test]
        public void DeleteReview_ShouldReturnError_WhenReviewIdIsZero()
        {
            var result = _repository.DeleteReview(0);

            _mockSet.Verify(m => m.Remove(It.IsAny<Review>()), Times.Never());
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("EventId not found"));
        }

        [Test]
        public void GetAllReviews_ShouldReturnAllReviews()
        {
            var result = _repository.GetAllReviews();
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetReviewsByEventId_ShouldReturnReviews_WhenEventIdExists()
        {
            var result = _repository.GetReviewsByEventId(101);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rating, Is.EqualTo(5));
        }

        [Test]
        public void GetReviewsByEventId_ShouldReturnNull_WhenEventIdDoesNotExist()
        {
            var result = _repository.GetReviewsByEventId(999);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void UpdateReview_ShouldUpdateReview_WhenReviewExists()
        {
            var updatedReview = new Review { ReviewId = 1, Rating = 4, Comment = "Updated comment", ReviewDate = DateTime.Now };

            var result = _repository.UpdateReview(updatedReview);

            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("Updated Successfully"));
        }

        [Test]
        public void UpdateReview_ShouldReturnError_WhenReviewDoesNotExist()
        {
            var updatedReview = new Review { ReviewId = 99, Rating = 4, Comment = "Nonexistent comment", ReviewDate = DateTime.Now };

            var result = _repository.UpdateReview(updatedReview);

            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("Error Updating Review"));
        }
    }
}


