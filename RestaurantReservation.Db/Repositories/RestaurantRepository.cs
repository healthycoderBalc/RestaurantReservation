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
    public class RestaurantRepository : IDisposable, IRestaurantRepository
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public RestaurantRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            return await _dbContext.Restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetRestaurantAsync(int restaurantId, bool includeLists)
        {
            if (includeLists)
            {
                return await _dbContext.Restaurants
                    .Include(r => r.Tables)
                    .Include(r => r.Employees)
                    .Include(r => r.MenuItems)
                    .Include(r => r.Reservations)
                    .Where(r => r.RestaurantId == restaurantId)
                    .FirstOrDefaultAsync();
            }
            return await _dbContext.Restaurants
                .Where(r => r.RestaurantId == restaurantId)
                .FirstOrDefaultAsync();
        }


        public void CreateRestaurantAsync(Restaurant restaurant)
        {
            _dbContext.Restaurants.Add(restaurant);

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

        public void DeleteRestaurantAsync(Restaurant restaurant)
        {
            _dbContext.Restaurants.Remove(restaurant);
        }

        public async Task<decimal> GetRestaurantTotalRevenueAsync(int restaurantId)
        {
            decimal restaurantTotalRevenue = _dbContext.Restaurants
                .Where(r => r.RestaurantId == restaurantId)
                .Select(r => RestaurantReservationDbContext.GetRestaurantTotalRevenue(restaurantId))
                .FirstOrDefault();

            return restaurantTotalRevenue;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
