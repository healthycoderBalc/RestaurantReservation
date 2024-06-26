using RestaurantReservation.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class OrderItemFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task DemonstrateAsync()
        {
            using (var orderItemDemonstration = new OrderItemRepository())
            {
                Console.WriteLine("----------ORDER ITEM DEMONSTRATION---------");

                int orderId = 1;
                int itemId = 1;
                int quantity = 2;
                int createdOrderItemId = await orderItemDemonstration.CreateOrderItemAsync(orderId, itemId, quantity);
                await orderItemDemonstration.ReadOrderItemAsync(createdOrderItemId);

                int newOrderId = 1;
                int newItemId = 1;
                int newQuantity = 3;
                await orderItemDemonstration.UpdateOrderItemAsync(createdOrderItemId, newOrderId, newItemId, newQuantity);
                await orderItemDemonstration.ReadOrderItemAsync(createdOrderItemId);

                await orderItemDemonstration.DeleteOrderItemAsync(createdOrderItemId);
                await orderItemDemonstration.ReadOrderItemAsync(createdOrderItemId);
            }
        }
    }
}
