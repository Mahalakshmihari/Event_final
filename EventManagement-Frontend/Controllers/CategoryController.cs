using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Frontend.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategory();
            return View(categories); 
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View(); 
        }

        // POST: Category/Create
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
               
                var result = await _categoryService.CreateCategory(category.CategoryName);
                if (result)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to Index on success
                }
                ModelState.AddModelError("", "Error creating category. Please try again."); // Show error
            }
            return View(category); 
        }


        // POST: Category/Edit/{id}
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id); 
            if (category == null)
            {
                return NotFound();
            }
            return View(category); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditData(int id,CategoryModel category)
        {
         
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateCategory(id, category);
                if (result)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                ModelState.AddModelError("", "Error updating category. Please try again."); 
            }
            return View(category); 
        }

        // POST: Category/Delete/{id}
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result)
            {
                return NotFound(); // Return 404 if category not found
            }
            return RedirectToAction(nameof(Index)); // Redirect to Index on success
        }
    }
}

