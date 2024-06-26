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
    public class OrderRepository : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public OrderRepository()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public async Task<int> CreateOrder(int reservationId, int employeeId, DateTime orderDate, decimal totalAmount)
        {
            var reservation = await _dbContext.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return 0;
            }
            var employee = await _dbContext.Employees.FindAsync(employeeId);

            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return 0;
            }

            var newOrder = new Order
            {
                Reservation = reservation,
                Employee = employee,
                OrderDate = orderDate,
                TotalAmount = totalAmount
            };

            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Order created with ID: {newOrder.OrderId}");

            return newOrder.OrderId;
        }

        public async Task ReadOrder(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);

            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            Console.WriteLine($"Order found: ID {order.OrderId} - Reservation Nº: {order.ReservationId}, Employee: {order.Employee.FirstName} {order.Employee.LastName}, Date: {order.OrderDate}, Total Amount: {order.TotalAmount}");
        }

        public async Task UpdateOrder(int orderId, int reservationId, int employeeId, DateTime orderDate, decimal totalAmount)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            var newReservation = await _dbContext.Reservations.FindAsync(reservationId);
            if (newReservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return;
            }
            var newEmployee = await _dbContext.Employees.FindAsync(employeeId);

            if (newEmployee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return;
            }

            order.Reservation = newReservation;
            order.Employee = newEmployee;
            order.OrderDate = orderDate;
            order.TotalAmount = totalAmount;

            _dbContext.SaveChanges();
            Console.WriteLine($"Order {orderId} updated successfully.");
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Order {orderId} deleted successfully.");
        }

        public async Task<List<Order>> ListOrderAndMenuItems(int reservationId)
        {
            List<Order> orders = await _dbContext.Orders
                .Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            return orders;
        }

        public async Task<List<MenuItem>> ListOrderedMenuItems(int reservationId)
        {
            List<MenuItem> menuItems= await _dbContext.OrderItems
                .Where(oi => oi.Order.ReservationId == reservationId)
                .Select(oi => oi.MenuItem)
                .ToListAsync();

            return menuItems;
        }

        public async Task<decimal> CalculateAverageOrderAmount(int employeeId)
        {
            List<Order> employeeOrders = await _dbContext.Orders
                .Where(o => o.EmployeeId == employeeId)
                .ToListAsync();

            if (employeeOrders.Count == 0) return 0;
            decimal average = employeeOrders
                .Average(o => o.TotalAmount);

            return average;
        }
    }
}
