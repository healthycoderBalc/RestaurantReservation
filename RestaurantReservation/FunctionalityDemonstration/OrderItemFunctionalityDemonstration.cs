using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class OrderItemFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public void Demonstrate()
        {
            using (var orderItemDemonstration = new OrderItemRepository())
            {
                int orderId = 1;
                int itemId = 1;
                int quantity = 2;
                int createdOrderItemId = orderItemDemonstration.CreateOrderItem(orderId, itemId, quantity);
                orderItemDemonstration.ReadOrderItem(createdOrderItemId);

                int newOrderId = 1;
                int newItemId = 1;
                int newQuantity = 3;
                orderItemDemonstration.UpdateOrderItem(createdOrderItemId, newOrderId, newItemId, newQuantity);
                orderItemDemonstration.ReadOrderItem(createdOrderItemId);

                orderItemDemonstration.DeleteOrderItem(createdOrderItemId);
                orderItemDemonstration.ReadOrderItem(createdOrderItemId);
            }
        }
    }
}
