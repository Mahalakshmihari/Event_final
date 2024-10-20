using EventManagement_Frontend.Models;

namespace EventManagement_Frontend.IService
{
    public interface IEventService
    {
        Task<List<EventModel>> GetEvents();
        Task<bool> DeleteEvents(int eventId);
        Task<bool> UpdateEvents(int eventId, EventModel eventCategory);
        Task<bool> CreateEvents(EventModel eventItem);
        Task<EventModel> GetEventsById(int eventId);
    }
}
