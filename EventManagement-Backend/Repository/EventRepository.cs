using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;

namespace EventManagement_Backend.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly EventManagementDbContext _context;

        public EventRepository(EventManagementDbContext context)
        {
            _context = context;
        }
        public List<Event> GetAllEvents()
        {

            return _context.Events.ToList();
            // Fetch all events from the database
        }
        public Event GetEventById(int id)
        {
            var item = _context.Events.Where(r => r.EventId == id).FirstOrDefault();
            if (item != null)
            {
                return item;
            }
            else
            {
                return null;
            }

            // Fetch a specific event by ID
        }
        public string CreateEvent(Event eventEntity)
        {
            if (eventEntity != null)
            {
                _context.Events.Add(eventEntity);
                _context.SaveChanges();
                return "added successfully";
            }
            else
            {
                return "somethinng went wrong";
            }
        }
        public string UpdateEvent(Event updatedeventEntity)
        {
            if (updatedeventEntity != null)
            {
                var item = _context.Events.FirstOrDefault(r => r.EventId == updatedeventEntity.EventId);
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(updatedeventEntity.Description))
                    {
                        item.EventName = updatedeventEntity.EventName;
                    }
                    item.Price = updatedeventEntity.Price;
                     item.Description = updatedeventEntity.Description;
                    
                    if (!string.IsNullOrEmpty(updatedeventEntity.EventName))
                    {
                        item.Location = updatedeventEntity.Location;
                    }
                    if (updatedeventEntity.Rating!=null)
                    {
                        item.Rating = updatedeventEntity.Rating;
                    }
                    item.StartDate = updatedeventEntity.StartDate;
                    item.EndDate = updatedeventEntity.EndDate;
                    item.Category = updatedeventEntity.Category;
                    item.ImageUrl = updatedeventEntity.ImageUrl;
                    _context.SaveChanges();
                    return "updated successfully";
                }
                else
                {
                    return "not updated";
                }
            }
            else
            {
                return "somethinng went wrong";
            }

        }
        public string DeleteEvent(int id)
        {
            var eventEntity = _context.Events.FirstOrDefault(r => r.EventId == id);
            if (eventEntity != null)
            {
                _context.Events.Remove(eventEntity); // Remove the event from the context
                _context.SaveChangesAsync(); // Save changes to the database
                return "removed successfully";
            }
            else
            {
                return "not removed";
            }
        }
        public Event GetByName(string name)
        {
            var item = _context.Events.FirstOrDefault(r => r.EventName == name);
            if (item != null)
            {
                return item;
            }
            else
            {
                return null;
            }
        }
    }
}
