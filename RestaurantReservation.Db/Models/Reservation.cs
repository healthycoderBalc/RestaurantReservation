using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models
{
    public class Reservation
    {
        public Reservation() {
            Orders = new List<Order>();
        }

        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int PartySize { get; set; }

        public Customer Customer { get; set; }
        public Restaurant Restaurant { get; set; }
        public Table Table { get; set; }
        public List<Order> Orders { get; set; }
    }
}
