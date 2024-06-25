using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class RestaurantFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public void Demonstrate()
        {
            using (var restaurantDemonstration = new RestaurantRepository())
            {
                string name = "Buonjorno";
                string address = "123 Main St";
                string phoneNumber = "555-1234";
                string openingHours = "9AM - 9PM";
                int createdRestaurantId = restaurantDemonstration.CreateRestaurant(name, address, phoneNumber, openingHours);
                restaurantDemonstration.ReadRestaurant(createdRestaurantId);

                string newAddress = "456 Yellow St";
                string newPhoneNumber = "555-5678";
                string newOpeningHours = "9AM - 7PM";
                restaurantDemonstration.UpdateRestaurant(createdRestaurantId, newAddress, newPhoneNumber, newOpeningHours);
                restaurantDemonstration.ReadRestaurant(createdRestaurantId);

                restaurantDemonstration.DeleteRestaurant(createdRestaurantId);
                restaurantDemonstration.ReadRestaurant(createdRestaurantId);
            }
        }
    }
}
