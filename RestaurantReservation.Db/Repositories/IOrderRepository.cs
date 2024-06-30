using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IOrderRepository
    {
        Task<decimal> CalculateAverageOrderAmountAsync(int employeeId);
        Task CreateOrderAsync(int reservationId, int employeeId, Order order);
        void DeleteOrderAsync(Order order);
        Task<Order?> GetOrderAsync(int orderId, bool includeOrderItems);
        Task<List<Order>> GetOrdersAsync();
        Task<List<Order>> ListOrderAndMenuItemsAsync(int reservationId);
        Task<List<MenuItem>> ListOrderedMenuItemsAsync(int reservationId);
        Task UpdateOrderAsync(int orderId, int reservationId, int employeeId, DateTime orderDate, decimal totalAmount);

        Task<bool> ReservationAndEmployeeExistsAsync(int reservationId, int employeeId);
        Task<bool> SaveChangesAsync();
    }
}