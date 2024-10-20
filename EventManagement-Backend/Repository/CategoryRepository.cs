using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement_Backend.Repository
{
    public class CategoryRepository : ICaterogryRepository
    {
        private readonly EventManagementDbContext _context;
        public CategoryRepository(EventManagementDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="category">The category to be added.</param>
        /// <returns>A message indicating whether the category was added successfully or not.</returns>
        public string AddCategory(Category category)
        {
            if (category != null)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return "Category Added Successfully";
            }
            else
            {
                return "Error Adding Category";
            }
            
        }
        /// <summary>
        /// Retrieves a list of all categories from the database.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category that matches the provided ID, or null if not found.</returns>
        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }
        /// <summary>
        /// Removes a category from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to remove.</param>
        /// <returns>A message indicating whether the category was removed successfully or not.</returns>
        public string RemoveCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return "Category Removed Successfully";
            }
            else
            {
                return "Error Category Removing";
            }
        }
        /// <summary>
        /// Updates an existing category's details.
        /// </summary>
        /// <param name="category">The category object with updated information.</param>
        /// <returns>A message indicating whether the category was updated successfully or not.</returns>
        public string UpdateCategory(Category category)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();
                return "Updated SuucessFully";
            }
            else
            {
                return "Error Updating Category";
            }
        }
    }
}
