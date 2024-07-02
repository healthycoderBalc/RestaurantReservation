using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IOrderRepository
    {
       
        Task<List<Order>> GetOrdersAsync();
        Task<Order?> GetOrderAsync(int orderId, bool includeOrderItems);
        Task CreateOrderAsync(int reservationId, int employeeId, Order order);
        void DeleteOrderAsync(Order order);

        Task<bool> ReservationAndEmployeeExistsAsync(int reservationId, int employeeId);
        Task<bool> SaveChangesAsync();
    }
}