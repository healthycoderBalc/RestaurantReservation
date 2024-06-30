using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetMenuItemsAsync();
        Task<MenuItem?> GetMenuItemAsync(int menuItemId, bool includeMenuItems);
        Task CreateMenuItemAsync(int restaurantId, MenuItem menuItem);
        Task UpdateMenuItemAsync(int menuItemId, int restaurantId, string name, string description, decimal price);
        void DeleteMenuItemAsync(MenuItem menuItemId);
        Task<bool> RestaurantExistsAsync(int restaurantId);
        Task<bool> SaveChangesAsync();
    }
}