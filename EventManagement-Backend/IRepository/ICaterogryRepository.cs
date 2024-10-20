using EventManagement_Backend.Models;

namespace EventManagement_Backend.IRepository
{
    public interface ICaterogryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);
        string AddCategory(Category category);
        string UpdateCategory(Category category);
        string RemoveCategory(int id);
    }
}
