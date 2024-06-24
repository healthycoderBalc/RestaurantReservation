using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models
{
    public class Restaurant
    {
        public Restaurant()
        {
            Tables = new List<Table>();
            Employees = new List<Employee>();
            MenuItems = new List<MenuItem>();
            Reservations = new List<Reservation>();
        }


        [Key]
        public int RestaurantId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string OpeningHours { get; set; }

        public List<Table> Tables { get; set; }
        public List<Employee> Employees { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
