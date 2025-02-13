using API5.Models;

namespace API5.Repositories
{
    public interface IRepository
    {
        IEnumerable<Reservation> Reservations { get; }

        Reservation this[int id] { get; }

        Reservation AddReservation(Reservation reservation);

        Reservation UpdateReservation(Reservation reservation);

        void DeleteReservation(int id);
    }
}
