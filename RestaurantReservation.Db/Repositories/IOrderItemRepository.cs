using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetOrderItemsAsync();
        Task<OrderItem?> GetOrderItemAsync(int orderItemId);
        Task CreateOrderItemAsync(int orderId, int itemId, OrderItem orderItem);
        Task UpdateOrderItemAsync(int orderItemId, int orderId, int itemId, int quantity);
        void DeleteOrderItemAsync(OrderItem orderItem);

        Task<bool> OrderAndItemExistsAsync(int orderId, int menuItemId);
        Task<bool> SaveChangesAsync();
    }
}