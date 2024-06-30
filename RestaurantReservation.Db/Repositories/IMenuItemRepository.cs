using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IMenuItemRepository
    {
        Task CreateMenuItemAsync(int restaurantId, MenuItem menuItem);
        void DeleteMenuItemAsync(MenuItem menuItemId);
        Task<MenuItem?> GetMenuItemAsync(int menuItemId, bool includeMenuItems);
        Task<List<MenuItem>> GetMenuItemsAsync();
        Task UpdateMenuItemAsync(int menuItemId, int restaurantId, string name, string description, decimal price);
        Task<bool> RestaurantExistsAsync(int restaurantId);
        Task<bool> SaveChangesAsync();
    }
}