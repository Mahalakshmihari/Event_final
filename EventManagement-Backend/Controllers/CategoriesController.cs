using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;
using EventManagement_Backend.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICaterogryRepository _categoryService;

        public CategoriesController(ICaterogryRepository categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/category
        [HttpGet]

        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetCategories();
            return Ok(categories);
        }

        // GET: api/category/{id}
        
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _categoryService.AddCategory(category);
            return Ok("Category added successfully");
        }

        // PUT: api/category/{id}
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            category.CategoryId = id; // Ensure the correct ID is set
            _categoryService.UpdateCategory(category);

            return Ok("Category updated successfully");
        }

        //[Authorize(Roles = "Admin")]
        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            _categoryService.RemoveCategory(id);
            return Ok("Category deleted successfully");
        }
    }
}
