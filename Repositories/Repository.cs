using API5.Models;

namespace API5.Repositories
{
    public class Repository : IRepository
    {
        private Dictionary<int, Reservation> items;

        public Repository()
        {
            items = new Dictionary<int, Reservation>();
            new List<Reservation>
            {
                new Reservation { Id = 1, Name = "Thepv", StartLocation = "Ha Noi", EndLocation = "Can Tho" },
                new Reservation { Id = 2, Name = "Fpoly", StartLocation = "Sai Gon", EndLocation = "Tay Nguyen" }
            }.ForEach(r => AddReservation(r));
        }

        public IEnumerable<Reservation> Reservations => items.Values;

        public Reservation this[int id] => items.ContainsKey(id) ? items[id] : null;

        public Reservation AddReservation(Reservation reservation)
        {
            if (reservation == null || items.ContainsKey(reservation.Id))
                return null;

            items[reservation.Id] = reservation;
            return reservation;
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            if (reservation == null || !items.ContainsKey(reservation.Id))
                return null;

            items[reservation.Id] = reservation;
            return reservation;
        }

        public void DeleteReservation(int id)
        {
            if (items.ContainsKey(id))
            {
                items.Remove(id);
            }
        }
    }
}
