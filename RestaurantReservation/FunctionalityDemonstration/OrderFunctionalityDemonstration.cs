﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class OrderFunctionalityDemonstration : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public OrderFunctionalityDemonstration()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public int CreateOrder(int reservationId, int employeeId, DateTime orderDate, decimal totalAmount)
        {
            var reservation = _dbContext.Reservations.Find(reservationId);
            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return 0;
            }
            var employee = _dbContext.Employees.Find(employeeId);

            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return 0;
            }

            var newOrder = new Db.Models.Order
            {
                Reservation = reservation,
                Employee = employee,
                OrderDate = orderDate,
                TotalAmount = totalAmount
            };

            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();
            return newOrder.OrderId;
        }



        public void ReadOrder(int orderId)
        {
            var order = _dbContext.Orders.Find(orderId);

            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            Console.WriteLine($"Order found: Resrvation Nº: {order.ReservationId}, Employee: {order.Employee.FirstName} {order.Employee.LastName}, Date: {order.OrderDate}, Total Amount: {order.TotalAmount}");
        }

        public void UpdateOrder(int orderId, int reservationId, int employeeId, DateTime orderDate, decimal totalAmount)
        {
            var order = _dbContext.Orders.Find(orderId);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            var newReservation = _dbContext.Reservations.Find(reservationId);
            if (newReservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return;
            }
            var newEmployee = _dbContext.Employees.Find(employeeId);

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

        public void DeleteOrder(int orderId)
        {
            var order = _dbContext.Orders.Find(orderId);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }
            _dbContext.Remove(order);
            _dbContext.SaveChanges();
            Console.WriteLine($"Order {orderId} deleted successfully.");
        }
    }
}
