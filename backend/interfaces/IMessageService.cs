using data.Models;
namespace backend.interfaces
{
    public interface IMessageService
    {
        bool Enqueue(Reservation reservation);
    }
}