namespace EventManagement_Backend.CustomException
{
    public class BookingNotFoundException :Exception
    {
        public BookingNotFoundException(string message) : base(message)
        {
        }
    }
}
