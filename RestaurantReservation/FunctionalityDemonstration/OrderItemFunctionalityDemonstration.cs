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
        public async Task Demonstrate()
        {
            using (var orderItemDemonstration = new OrderItemRepository())
            {
                Console.WriteLine("----------ORDER ITEM DEMONSTRATION---------");

                int orderId = 1;
                int itemId = 1;
                int quantity = 2;
                int createdOrderItemId = await orderItemDemonstration.CreateOrderItem(orderId, itemId, quantity);
                await orderItemDemonstration.ReadOrderItem(createdOrderItemId);

                int newOrderId = 1;
                int newItemId = 1;
                int newQuantity = 3;
                await orderItemDemonstration.UpdateOrderItem(createdOrderItemId, newOrderId, newItemId, newQuantity);
                await orderItemDemonstration.ReadOrderItem(createdOrderItemId);

                await orderItemDemonstration.DeleteOrderItem(createdOrderItemId);
                await orderItemDemonstration.ReadOrderItem(createdOrderItemId);
            }
        }
    }
}
