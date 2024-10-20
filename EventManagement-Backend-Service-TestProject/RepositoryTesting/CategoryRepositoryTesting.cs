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
    public class CategoryRepositoryTests
    {
        private Mock<DbSet<Category>> _mockSet;
        private Mock<EventManagementDbContext> _mockContext;
        private CategoryRepository _repository;

        [SetUp]
        public void Setup()
        {
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Test Category" },
                new Category { CategoryId = 2, CategoryName = "Another Category" }
            };

            var queryableCategories = categories.AsQueryable();
            _mockSet = CreateDbSetMock(queryableCategories);
            _mockContext = new Mock<EventManagementDbContext>();
            _mockContext.Setup(m => m.Categories).Returns(_mockSet.Object);
            _repository = new CategoryRepository(_mockContext.Object);
        }

        public static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> elements) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elements.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elements.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elements.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elements.GetEnumerator());
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => elements.FirstOrDefault(d => (int)d.GetType().GetProperty("CategoryId").GetValue(d, null) == (int)ids[0]));
            return dbSetMock;
        }

        [Test]
        public void AddCategory_ShouldAddCategory_WhenCategoryIsValid()
        {
            var category = new Category { CategoryId = 1, CategoryName = "Test Category" };

            var result = _repository.AddCategory(category);

            _mockSet.Verify(m => m.Add(It.IsAny<Category>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("Category Added Successfully"));
        }

        [Test]
        public void AddCategory_ShouldReturnError_WhenCategoryIsNull()
        {
            Category category = null;

            var result = _repository.AddCategory(category);

            _mockSet.Verify(m => m.Add(It.IsAny<Category>()), Times.Never());
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("Error Adding Category"));
        }

        [Test]
        public void GetCategories_ShouldReturnListOfCategories()
        {
            var result = _repository.GetCategories().ToList();
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetCategoryById_ShouldReturnCategory_WhenCategoryExists()
        {
            var result = _repository.GetCategoryById(1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CategoryName, Is.EqualTo("Test Category"));
        }

        [Test]
        public void GetCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            var data = new List<Category>().AsQueryable();
            _mockSet = CreateDbSetMock(data);
            _mockContext.Setup(m => m.Categories).Returns(_mockSet.Object);
            _repository = new CategoryRepository(_mockContext.Object);

            var result = _repository.GetCategoryById(1);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void RemoveCategory_ShouldRemoveCategory_WhenCategoryExists()
        {
            var result = _repository.RemoveCategory(1);
            _mockSet.Verify(m => m.Remove(It.IsAny<Category>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("Category Removed SuccessFully"));
        }

        [Test]
        public void RemoveCategory_ShouldReturnError_WhenCategoryDoesNotExist()
        {
            var data = new List<Category>().AsQueryable();
            _mockSet = CreateDbSetMock(data);
            _mockContext.Setup(m => m.Categories).Returns(_mockSet.Object);
            _repository = new CategoryRepository(_mockContext.Object);

            var result = _repository.RemoveCategory(1);
            _mockSet.Verify(m => m.Remove(It.IsAny<Category>()), Times.Never());
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("Error Category Removing"));
        }

        [Test]
        public void UpdateCategory_ShouldUpdateCategory_WhenCategoryExists()
        {
            var existingCategory = new Category { CategoryId = 1, CategoryName = "Old Category" };
            var data = new List<Category> { existingCategory }.AsQueryable();
            _mockSet = CreateDbSetMock(data);
            _mockContext.Setup(m => m.Categories).Returns(_mockSet.Object);
            _repository = new CategoryRepository(_mockContext.Object);

            var updatedCategory = new Category { CategoryId = 1, CategoryName = "Updated Category" };

            var result = _repository.UpdateCategory(updatedCategory);

            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.That(result, Is.EqualTo("Updated SuucessFully"));
        }

        [Test]
        public void UpdateCategory_ShouldReturnError_WhenCategoryDoesNotExist()
        {
            var updatedCategory = new Category { CategoryId = 99, CategoryName = "Updated Category" };

            var result = _repository.UpdateCategory(updatedCategory);

            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
            Assert.That(result, Is.EqualTo("Error Updating Category"));
        }
    }
}








