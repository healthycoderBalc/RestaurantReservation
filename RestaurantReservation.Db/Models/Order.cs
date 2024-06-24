using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public int OrderId { get; set; }
        public int ReservationId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        
        public Reservation Reservation { get; set; }
        public Employee Employee { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
