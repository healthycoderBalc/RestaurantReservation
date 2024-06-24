using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models
{
    public class Table
    {
        public Table ()
        {
            Reservations = new List<Reservation> ();
        }

        [Key]
        public int TableId { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        
        [Required]
        public int Capacity { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; }

        public List<Reservation> Reservations { get; set; }

    }
}
