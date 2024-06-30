using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IReservationRepository
    {
        Task CreateReservationAsync(int customerId, int restaurantId, int tableId, Reservation reservation);
        void DeleteReservationAsync(Reservation reservation);
        Task<Reservation?> GetReservationAsync(int reservationId, bool includeOrders);
        Task<List<Reservation>> GetReservationsAsync();
        Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId);
        Task<List<ReservationWithDetails>> GetReservationsWithCustomerAndRestaurantInformationFromViewAsync();
        Task UpdateReservationAsync(int reservationId, int customerId, int restaurantId, int tableId, DateTime reservationDate, int partySize);

        Task<bool> CustomerRestaurantAndTableExistsAsync(int customerId, int restaurantId, int tableId);
        Task<bool> SaveChangesAsync();

    }
}