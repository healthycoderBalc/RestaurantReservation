using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class OrderFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task DemonstrateAsync()
        {
            using (var orderDemonstration = new OrderRepository())
            {
                Console.WriteLine("----------ORDER DEMONSTRATION---------");
                int reservationId = 1;
                int employeeId = 1;
                DateTime orderDate = DateTime.Now.AddDays(6);
                decimal totalAmount = 48.3M;
                int createdOrderId = await orderDemonstration.CreateOrderAsync(reservationId, employeeId, orderDate, totalAmount);
                await orderDemonstration.ReadOrderAsync(createdOrderId);

                int newReservationId = 1;
                int newEmployeeId = 1;
                DateTime newOrderDate = DateTime.Now.AddDays(8);
                decimal newTotalAmount = 49.3M;
                await orderDemonstration.UpdateOrderAsync(createdOrderId, newReservationId, newEmployeeId, newOrderDate, newTotalAmount);
                await orderDemonstration.ReadOrderAsync(createdOrderId);

                await orderDemonstration.DeleteOrderAsync(createdOrderId);
                await orderDemonstration.ReadOrderAsync(createdOrderId);

                Console.WriteLine();
                Console.WriteLine("List Order And Menu Items");
                List<Order> orders = await orderDemonstration.ListOrderAndMenuItemsAsync(reservationId);
                if (orders.Count > 0)
                {

                    foreach (Order order in orders)
                    {
                        Console.WriteLine($"Order ID: {order.OrderId}, Order Date: {order.OrderDate}");
                        Console.WriteLine("Order Items:");
                        foreach (OrderItem orderItem in order.OrderItems)
                        {
                            Console.WriteLine($" - Menu Item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}");
                        }
                    }
                }
                Console.WriteLine();

                Console.WriteLine("List Ordered Menu Items");
                List<MenuItem> menuItems = await orderDemonstration.ListOrderedMenuItemsAsync(reservationId);
                if (menuItems.Count > 0)
                {
                    foreach (MenuItem menuItem in menuItems)
                    {
                        Console.WriteLine($"Menu Item ID: {menuItem.MenuItemId}, Name: {menuItem.Name}, Price: {menuItem.Price}");
                    }
                }
                Console.WriteLine();

                Console.WriteLine("Calculate Average Order Amount");
                decimal averageOrderAmount = await orderDemonstration.CalculateAverageOrderAmountAsync(employeeId);

                Console.WriteLine($"Employee ID: {employeeId}, Average Order Amount: ${averageOrderAmount}");

                Console.WriteLine();
            }
        }
    }
}
