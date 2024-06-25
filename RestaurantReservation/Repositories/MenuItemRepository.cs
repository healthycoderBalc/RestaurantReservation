using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Repositories
{
    public class MenuItemRepository : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public MenuItemRepository()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public async Task<int> CreateMenuItem(int restaurantId, string name, string description, decimal price)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return 0;
            }

            var newMenuItem = new MenuItem
            {
                Restaurant = restaurant,
                Name = name,
                Description = description,
                Price = price
            };

            _dbContext.MenuItems.Add(newMenuItem);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Menu Item created with ID: {newMenuItem.MenuItemId}");

            return newMenuItem.MenuItemId;
        }


        public async Task ReadMenuItem(int menuItemId)
        {
            var menuItem = await _dbContext.MenuItems.FindAsync(menuItemId);

            if (menuItem == null)
            {
                Console.WriteLine($"MenuItem with ID {menuItemId} not found.");
                return;
            }
            Console.WriteLine($"MenuItem found: ID {menuItem.MenuItemId} - In Restaurant: {menuItem.Restaurant.Name}, Name: {menuItem.Name}, Description: {menuItem.Description}, ${menuItem.Price}");
        }

        public async Task UpdateMenuItem(int menuItemId, int restaurantId, string name, string description, decimal price)
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

        public async Task DeleteMenuItem(int menuItemId)
        {
            var menuItem = await _dbContext.MenuItems.FindAsync(menuItemId);
            if (menuItem == null)
            {
                Console.WriteLine($"MenuItem with ID {menuItemId} not found.");
                return;
            }
            _dbContext.Remove(menuItem);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"MenuItem {menuItemId} deleted successfully.");
        }
    }
}
