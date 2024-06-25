using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class MenuItemFunctionalityDemonstration : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public MenuItemFunctionalityDemonstration()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public int CreateMenuItem(int restaurantId, string name, string description, decimal price)
        {
            var restaurant = _dbContext.Restaurants.Find(restaurantId);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return 0;
            }

            var newMenuItem = new Db.Models.MenuItem
            {
                Restaurant = restaurant,
                Name = name,
                Description = description,
                Price = price
            };

            _dbContext.MenuItems.Add(newMenuItem);
            _dbContext.SaveChanges();
            return newMenuItem.MenuItemId;
        }



        public void ReadMenuItem(int menuItemId)
        {
            var menuItem = _dbContext.MenuItems.Find(menuItemId);

            if (menuItem == null)
            {
                Console.WriteLine($"MenuItem with ID {menuItemId} not found.");
                return;
            }
            Console.WriteLine($"MenuItem found: In Restaurant: {menuItem.Restaurant.Name}, Name: {menuItem.Name}, Description: {menuItem.Description}, ${menuItem.Price}");
        }

        public void UpdateMenuItem(int menuItemId, int restaurantId, string name, string description, decimal price)
        {
            var menuItem = _dbContext.MenuItems.Find(menuItemId);
            var newRestaurant = _dbContext.Restaurants.Find(restaurantId);
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

            _dbContext.SaveChanges();
            Console.WriteLine($"MenuItem {menuItemId} updated successfully.");
        }

        public void DeleteMenuItem(int menuItemId)
        {
            var menuItem = _dbContext.MenuItems.Find(menuItemId);
            if (menuItem == null)
            {
                Console.WriteLine($"MenuItem with ID {menuItemId} not found.");
                return;
            }
            _dbContext.Remove(menuItem);
            _dbContext.SaveChanges();
            Console.WriteLine($"MenuItem {menuItemId} deleted successfully.");
        }
    }
}
