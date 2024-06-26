using RestaurantReservation.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class MenuItemFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task DemonstrateAsync()
        {
            using (var menuItemDemonstration = new MenuItemRepository())
            {
                Console.WriteLine("----------MENU ITEM DEMONSTRATION---------");

                int restaurantId = 1;
                string name = "BBQ";
                string description = "Asado Argentino";
                decimal price = 59.65M;
                int createdMenuItemId = await menuItemDemonstration.CreateMenuItemAsync(restaurantId, name, description, price);
                await menuItemDemonstration.ReadMenuItemAsync(createdMenuItemId);

                int newRestaurantId = 2;
                string newName = "Fried Chicken";
                string newDescription = "The Best Chicken in the Town";
                decimal newPrice = 39.65M;
                await menuItemDemonstration.UpdateMenuItemAsync(createdMenuItemId, newRestaurantId, newName, newDescription, newPrice);
                await menuItemDemonstration.ReadMenuItemAsync(createdMenuItemId);

                await menuItemDemonstration.DeleteMenuItemAsync(createdMenuItemId);
                await menuItemDemonstration.ReadMenuItemAsync(createdMenuItemId);
            }
        }
    }
}
