using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class RestaurantRepository : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public RestaurantRepository()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public async Task<int> CreateRestaurantAsync(string name, string address, string phoneNumber, string openingHours)
        {
            var newRestaurant = new Restaurant
            {
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                OpeningHours = openingHours
            };

            _dbContext.Restaurants.Add(newRestaurant);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Restaurant created with ID: {newRestaurant.RestaurantId}");

            return newRestaurant.RestaurantId;
        }

        public async Task ReadRestaurantAsync(int restaurantId)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);

            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            Console.WriteLine($"Restaurant found: ID {restaurant.RestaurantId} - {restaurant.Name}, Address: {restaurant.Address}, Opening Hours: {restaurant.OpeningHours}, Phone: {restaurant.PhoneNumber}");
        }

        public async Task UpdateRestaurantAsync(int restaurantId, string newAddress, string newPhoneNumber, string newOpeningHours)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            restaurant.Address = newAddress;
            restaurant.PhoneNumber = newPhoneNumber;
            restaurant.OpeningHours = newOpeningHours;

            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Restaurant {restaurantId} updated successfully.");
        }

        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            _dbContext.Remove(restaurant);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Restaurant {restaurantId} deleted successfully.");
        }

        public async Task<decimal> GetRestaurantTotalRevenueAsync(int restaurantId)
        {
            decimal restaurantTotalRevenue = _dbContext.Restaurants
                .Where(r => r.RestaurantId == restaurantId)
                .Select(r => RestaurantReservationDbContext.GetRestaurantTotalRevenue(restaurantId))
                .FirstOrDefault();

            return restaurantTotalRevenue;
        }
    }
}
