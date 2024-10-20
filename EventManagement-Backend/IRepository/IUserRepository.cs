using EventManagement_Backend.Models;

namespace EventManagement_Backend.IRepository
{
    public interface IUserRepository
    {
        List<AspNetUser> GetAllUsers();
    }
}
