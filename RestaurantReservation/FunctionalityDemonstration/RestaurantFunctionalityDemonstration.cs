using RestaurantReservation.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class RestaurantFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task Demonstrate()
        {
            using (var restaurantDemonstration = new RestaurantRepository())
            {
                Console.WriteLine("----------RESTAURANT DEMONSTRATION---------");

                string name = "Buonjorno";
                string address = "123 Main St";
                string phoneNumber = "555-1234";
                string openingHours = "9AM - 9PM";
                int createdRestaurantId = await restaurantDemonstration.CreateRestaurant(name, address, phoneNumber, openingHours);
                await restaurantDemonstration.ReadRestaurant(createdRestaurantId);

                string newAddress = "456 Yellow St";
                string newPhoneNumber = "555-5678";
                string newOpeningHours = "9AM - 7PM";
                await restaurantDemonstration.UpdateRestaurant(createdRestaurantId, newAddress, newPhoneNumber, newOpeningHours);
                await restaurantDemonstration.ReadRestaurant(createdRestaurantId);

                await restaurantDemonstration.DeleteRestaurant(createdRestaurantId);
                await restaurantDemonstration.ReadRestaurant(createdRestaurantId);

                Console.WriteLine();
                Console.WriteLine("Get Restaurant Total Revenue");
                int restaurantId = 1;
                decimal totalRevenue = await restaurantDemonstration.GetRestaurantTotalRevenue(restaurantId);
                Console.WriteLine($"Total revenue for restaurant with ID {restaurantId} is ${totalRevenue}");
            }
        }
    }
}
