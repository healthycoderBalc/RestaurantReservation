using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class OrderFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public void Demonstrate()
        {
            using (var orderDemonstration = new OrderRepository())
            {
                int reservationId = 1;
                int employeeId = 1;
                DateTime orderDate = DateTime.Now.AddDays(6);
                decimal totalAmount = 48.3M;
                int createdOrderId = orderDemonstration.CreateOrder(reservationId, employeeId, orderDate, totalAmount);
                orderDemonstration.ReadOrder(createdOrderId);

                int newReservationId = 1;
                int newEmployeeId = 1;
                DateTime newOrderDate = DateTime.Now.AddDays(8);
                decimal newTotalAmount = 49.3M;
                orderDemonstration.UpdateOrder(createdOrderId, newReservationId, newEmployeeId, newOrderDate, newTotalAmount);
                orderDemonstration.ReadOrder(createdOrderId);

                orderDemonstration.DeleteOrder(createdOrderId);
                orderDemonstration.ReadOrder(createdOrderId);
            }
        }
    }
}
