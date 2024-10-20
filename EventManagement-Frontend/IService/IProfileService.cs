using EventManagement_Frontend.Models;

namespace EventManagement_Frontend.IService
{
    public interface IProfileService
    {
        Task<List<ProfileModel>> GetAllProfile();

    }
}
