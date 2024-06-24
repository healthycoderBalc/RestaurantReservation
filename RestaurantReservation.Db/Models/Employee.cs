using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models
{
    public class Employee
    {
        public Employee()
        {
            Orders = new List<Order>();
        }

        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; }

        public List<Order> Orders { get; set; }
    }
}
