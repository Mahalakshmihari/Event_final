using EventManagement_Backend.Models;

namespace EventManagement_Backend.IRepository
{
    public interface IEventRepository
    {
        List<Event> GetAllEvents();        // Retrieve all events
        Event GetEventById(int id);               // Retrieve a specific event by ID
        string CreateEvent(Event events);     // Create a new event
        string UpdateEvent(Event eventEntity);     // Update an existing event
        string DeleteEvent(int id);
        Event GetByName(string name);
    }
}
