using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class MenuItemRepository : IDisposable, IMenuItemRepository
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public MenuItemRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<MenuItem>> GetMenuItemsAsync()
        {
            return await _dbContext.MenuItems
                .Include(mi => mi.Restaurant)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemAsync(int menuItemId, bool includeLists)
        {
            if (includeLists)
            {
                return await _dbContext.MenuItems
                    .Include(mi => mi.Restaurant)
                    .Include(mi => mi.OrderItems)
                    .Where(mi => mi.MenuItemId == menuItemId)
                    .FirstOrDefaultAsync();
            }
            return await _dbContext.MenuItems
                .Include(mi => mi.Restaurant)
                .Where(mi => mi.MenuItemId == menuItemId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateMenuItemAsync(int restaurantId, MenuItem menuItem)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                _dbContext.MenuItems.Add(menuItem);
            }
        }

        public async Task ReadMenuItemAsync(int menuItemId)
        {
            var menuItem = await _dbContext.MenuItems.FindAsync(menuItemId);

            if (menuItem == null)
            {
                Console.WriteLine($"MenuItem with ID {menuItemId} not found.");
                return;
            }
            Console.WriteLine($"MenuItem found: ID {menuItem.MenuItemId} - In Restaurant: {menuItem.Restaurant.Name}, Name: {menuItem.Name}, Description: {menuItem.Description}, ${menuItem.Price}");
        }

        public async Task UpdateMenuItemAsync(int menuItemId, int restaurantId, string name, string description, decimal price)
        {
            var menuItem = await _dbContext.MenuItems.FindAsync(menuItemId);
            var newRestaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (menuItem == null)
            {
                Console.WriteLine($"MenuItem with ID {menuItemId} not found.");
                return;
            }
            if (newRestaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            menuItem.Restaurant = newRestaurant;
            menuItem.Name = name;
            menuItem.Description = description;
            menuItem.Price = price;

            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"MenuItem {menuItemId} updated successfully.");
        }

        public void DeleteMenuItemAsync(MenuItem menuItem)
        {
            _dbContext.MenuItems.Remove(menuItem);
        }

        public async Task<bool> RestaurantExistsAsync(int restaurantId)
        {
            return await _dbContext.Restaurants.AnyAsync(r => r.RestaurantId == restaurantId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
