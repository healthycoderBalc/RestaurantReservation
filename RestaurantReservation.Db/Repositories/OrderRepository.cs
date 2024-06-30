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
    public class OrderRepository : IDisposable, IOrderRepository
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public OrderRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.Reservation)
                .Include(o => o.Employee)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderAsync(int orderId, bool includeLists)
        {
            if (includeLists)
            {
                return await _dbContext.Orders
                    .Include(o => o.Reservation)
                    .Include(o => o.OrderItems)
                    .Where(o => o.OrderId == orderId)
                    .FirstOrDefaultAsync();
            }
            return await _dbContext.Orders
                .Include(o => o.Reservation)
                .Include(o => o.Employee)
                .Where(o => o.OrderId == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateOrderAsync(int reservationId, int employeeId, Order order)
        {
            var reservation = await _dbContext.Reservations.FindAsync(reservationId);
            var employee = await _dbContext.Employees.FindAsync(employeeId);

            if (reservation != null && employee != null)
            {
                _dbContext.Orders.Add(order);
            }
        }

        public async Task ReadOrderAsync(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);

            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            Console.WriteLine($"Order found: ID {order.OrderId} - Reservation Nº: {order.ReservationId}, Employee: {order.Employee.FirstName} {order.Employee.LastName}, Date: {order.OrderDate}, Total Amount: {order.TotalAmount}");
        }

        public async Task UpdateOrderAsync(int orderId, int reservationId, int employeeId, DateTime orderDate, decimal totalAmount)
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

        public void DeleteOrderAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
        }

      

        public async Task<bool> ReservationAndEmployeeExistsAsync(int reservationId, int employeeId)
        {
            var reservation = await _dbContext.Reservations
                .AnyAsync(r => r.ReservationId == reservationId);
            var employee = await _dbContext.Employees
                .AnyAsync(e => e.EmployeeId == employeeId);
            return reservation && employee;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
