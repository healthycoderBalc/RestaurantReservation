using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IOrderItemRepository
    {
        Task CreateOrderItemAsync(int orderId, int itemId, OrderItem orderItem);
        void DeleteOrderItemAsync(OrderItem orderItem);
        Task<OrderItem?> GetOrderItemAsync(int orderItemId);
        Task<List<OrderItem>> GetOrderItemsAsync();
        Task UpdateOrderItemAsync(int orderItemId, int orderId, int itemId, int quantity);

        Task<bool> OrderAndItemExistsAsync(int orderId, int menuItemId);
        Task<bool> SaveChangesAsync();
    }
}