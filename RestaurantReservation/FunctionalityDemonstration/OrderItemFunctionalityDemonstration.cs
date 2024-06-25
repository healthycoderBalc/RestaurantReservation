using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class OrderItemFunctionalityDemonstration : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public OrderItemFunctionalityDemonstration()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public int CreateOrderItem(int orderId, int itemId, int quantity)
        {
            var order = _dbContext.Orders.Find(orderId);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return 0;
            }
            var item = _dbContext.MenuItems.Find(itemId);
            if (item == null)
            {
                Console.WriteLine($"Item with ID {itemId} not found.");
                return 0;
            }

            var newOrderItem = new Db.Models.OrderItem
            {
                Order = order,
                MenuItem = item,
                Quantity = quantity
            };

            _dbContext.OrderItems.Add(newOrderItem);
            _dbContext.SaveChanges();
            return newOrderItem.OrderItemId;
        }



        public void ReadOrderItem(int orderItemId)
        {
            var orderItem = _dbContext.OrderItems.Find(orderItemId);

            if (orderItem == null)
            {
                Console.WriteLine($"OrderItem with ID {orderItemId} not found.");
                return;
            }
            Console.WriteLine($"OrderItem found: Order: {orderItem.OrderId}, Menu item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}");
        }

        public void UpdateOrderItem(int orderItemId, int orderId, int itemId, int quantity)
        {
            var orderItem = _dbContext.OrderItems.Find(orderItemId);
            if (orderItem == null)
            {
                Console.WriteLine($"Order Item with ID {orderItemId} not found.");
                return;
            }
            var newOrder = _dbContext.Orders.Find(orderId);
            if (newOrder == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            var newItem= _dbContext.MenuItems.Find(itemId);
            if (newItem== null)
            {
                Console.WriteLine($"Menu Item with ID {orderId} not found.");
                return;
            }
            orderItem.Order = newOrder;
            orderItem.MenuItem = newItem;
            orderItem.Quantity = quantity;

            _dbContext.SaveChanges();
            Console.WriteLine($"Order Item {orderItemId} updated successfully.");
        }

        public void DeleteOrderItem(int orderItemId)
        {
            var orderItem = _dbContext.OrderItems.Find(orderItemId);
            if (orderItem == null)
            {
                Console.WriteLine($"OrderItem with ID {orderItemId} not found.");
                return;
            }
            _dbContext.Remove(orderItem);
            _dbContext.SaveChanges();
            Console.WriteLine($"OrderItem {orderItemId} deleted successfully.");
        }
    }
}
