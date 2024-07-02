using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservationsAsync();
        Task<Reservation?> GetReservationAsync(int reservationId, bool includeOrders);
        Task CreateReservationAsync(int customerId, int restaurantId, int tableId, Reservation reservation);
        void DeleteReservationAsync(Reservation reservation);

        Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId);
        Task<List<Order>> ListOrderAndMenuItemsAsync(int reservationId);
        Task<List<MenuItem>> ListOrderedMenuItemsAsync(int reservationId);
        Task<List<ReservationWithDetails>> GetReservationsWithCustomerAndRestaurantInformationFromViewAsync();
        Task<bool> CustomerRestaurantAndTableExistsAsync(int customerId, int restaurantId, int tableId);
        Task<bool> CustomerExistAsync(int customerId);
        Task<bool> RestaurantExistAsync(int restaurantId);
        Task<bool> TableExistAsync(int tableId);
        Task<bool> SaveChangesAsync();
    }
}