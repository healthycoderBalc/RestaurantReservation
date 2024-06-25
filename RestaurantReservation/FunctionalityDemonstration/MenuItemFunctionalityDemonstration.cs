using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class MenuItemFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public void Demonstrate()
        {
            using (var menuItemDemonstration = new MenuItemRepository())
            {
                int restaurantId = 1;
                string name = "BBQ";
                string description = "Asado Argentino";
                decimal price = 59.65M;
                int createdMenuItemId = menuItemDemonstration.CreateMenuItem(restaurantId, name, description, price);
                menuItemDemonstration.ReadMenuItem(createdMenuItemId);

                int newRestaurantId = 2;
                string newName = "Fried Chicken";
                string newDescription = "The Best Chicken in the Town";
                decimal newPrice = 39.65M;
                menuItemDemonstration.UpdateMenuItem(createdMenuItemId, newRestaurantId, newName, newDescription, newPrice);
                menuItemDemonstration.ReadMenuItem(createdMenuItemId);

                menuItemDemonstration.DeleteMenuItem(createdMenuItemId);
                menuItemDemonstration.ReadMenuItem(createdMenuItemId);
            }
        }
    }
}
