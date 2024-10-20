namespace EventManagement_Backend.IRepository
{
    public interface ISeatRepository
    {
        bool UpdateSeat(int  seatId, List<int> seatIds);
    }
}
