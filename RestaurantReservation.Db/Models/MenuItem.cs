using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models
{
    public class MenuItem
    {
        public MenuItem()
        {
            OrderItems = new List<OrderItem>();
        }

        public int MenuItemId { get; set; }
        public int RestaurantId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Restaurant Restaurant { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
