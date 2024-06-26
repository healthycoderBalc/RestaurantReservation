using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderItemRepository : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public OrderItemRepository()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public async Task<int> CreateOrderItem(int orderId, int itemId, int quantity)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return 0;
            }
            var item = await _dbContext.MenuItems.FindAsync(itemId);
            if (item == null)
            {
                Console.WriteLine($"Item with ID {itemId} not found.");
                return 0;
            }

            var newOrderItem = new OrderItem
            {
                Order = order,
                MenuItem = item,
                Quantity = quantity
            };

            _dbContext.OrderItems.Add(newOrderItem);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Order Item created with ID: {newOrderItem.OrderItemId}");

            return newOrderItem.OrderItemId;
        }



        public async Task ReadOrderItem(int orderItemId)
        {
            var orderItem = await _dbContext.OrderItems.FindAsync(orderItemId);

            if (orderItem == null)
            {
                Console.WriteLine($"OrderItem with ID {orderItemId} not found.");
                return;
            }
            Console.WriteLine($"OrderItem found: ID {orderItem.OrderItemId} - Order: {orderItem.OrderId}, Menu item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}");
        }

        public async Task UpdateOrderItem(int orderItemId, int orderId, int itemId, int quantity)
        {
            var orderItem = await _dbContext.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                Console.WriteLine($"Order Item with ID {orderItemId} not found.");
                return;
            }
            var newOrder = await _dbContext.Orders.FindAsync(orderId);
            if (newOrder == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            var newItem = await _dbContext.MenuItems.FindAsync(itemId);
            if (newItem == null)
            {
                Console.WriteLine($"Menu Item with ID {orderId} not found.");
                return;
            }
            orderItem.Order = newOrder;
            orderItem.MenuItem = newItem;
            orderItem.Quantity = quantity;

            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Order Item {orderItemId} updated successfully.");
        }

        public async Task DeleteOrderItem(int orderItemId)
        {
            var orderItem = await _dbContext.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                Console.WriteLine($"OrderItem with ID {orderItemId} not found.");
                return;
            }
            _dbContext.Remove(orderItem);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"OrderItem {orderItemId} deleted successfully.");
        }
    }
}
