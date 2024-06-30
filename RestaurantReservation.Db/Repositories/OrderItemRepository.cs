using Microsoft.EntityFrameworkCore;
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
    public class OrderItemRepository : IDisposable, IOrderItemRepository
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public OrderItemRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<OrderItem>> GetOrderItemsAsync()
        {
            return await _dbContext.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.MenuItem)
                .ToListAsync();
        }

        public async Task<OrderItem?> GetOrderItemAsync(int orderItemId)
        {
            return await _dbContext.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.MenuItem)
                .Where(oi => oi.OrderItemId == orderItemId)
                .FirstOrDefaultAsync();
        }


        public async Task CreateOrderItemAsync(int orderId, int itemId, OrderItem orderItem)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            var item = await _dbContext.MenuItems.FindAsync(itemId);

            if (order != null && item != null)
            {
                _dbContext.OrderItems.Add(orderItem);
            }
        }



        public async Task ReadOrderItemAsync(int orderItemId)
        {
            var orderItem = await _dbContext.OrderItems.FindAsync(orderItemId);

            if (orderItem == null)
            {
                Console.WriteLine($"OrderItem with ID {orderItemId} not found.");
                return;
            }
            Console.WriteLine($"OrderItem found: ID {orderItem.OrderItemId} - Order: {orderItem.OrderId}, Menu item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}");
        }

        public async Task UpdateOrderItemAsync(int orderItemId, int orderId, int itemId, int quantity)
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

        public void DeleteOrderItemAsync(OrderItem orderItem)
        {
            _dbContext.OrderItems.Remove(orderItem);
        }

        public async Task<bool> OrderAndItemExistsAsync(int orderId, int menuItemId)
        {
            var order = await _dbContext.Orders
                .AnyAsync(o => o.OrderId == orderId);
            var menuItem = await _dbContext.MenuItems
                .AnyAsync(o => o.MenuItemId == menuItemId);
            return order && menuItem;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
