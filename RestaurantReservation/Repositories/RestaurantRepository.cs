using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Repositories
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

        public int CreateRestaurant(string name, string address, string phoneNumber, string openingHours)
        {
            var newRestaurant = new Restaurant
            {
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                OpeningHours = openingHours
            };

            _dbContext.Restaurants.Add(newRestaurant);
            _dbContext.SaveChanges();
            Console.WriteLine($"Restaurant created with ID: {newRestaurant.RestaurantId}");

            return newRestaurant.RestaurantId;
        }



        public void ReadRestaurant(int restaurantId)
        {
            var restaurant = _dbContext.Restaurants.Find(restaurantId);

            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            Console.WriteLine($"Restaurant found: ID {restaurant.RestaurantId} - {restaurant.Name}, Address: {restaurant.Address}, Opening Hours: {restaurant.OpeningHours}, Phone: {restaurant.PhoneNumber}");
        }

        public void UpdateRestaurant(int restaurantId, string newAddress, string newPhoneNumber, string newOpeningHours)
        {
            var restaurant = _dbContext.Restaurants.Find(restaurantId);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            restaurant.Address = newAddress;
            restaurant.PhoneNumber = newPhoneNumber;
            restaurant.OpeningHours = newOpeningHours;

            _dbContext.SaveChanges();
            Console.WriteLine($"Restaurant {restaurantId} updated successfully.");
        }

        public void DeleteRestaurant(int restaurantId)
        {
            var restaurant = _dbContext.Restaurants.Find(restaurantId);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            _dbContext.Remove(restaurant);
            _dbContext.SaveChanges();
            Console.WriteLine($"Restaurant {restaurantId} deleted successfully.");
        }
    }
}
