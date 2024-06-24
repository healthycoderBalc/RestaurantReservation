using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Models
{
    public class Customer
    {
        public Customer()
        {
            Reservations = new List<Reservation>();
        }

        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public List<Reservation> Reservations { get; set; }

    }
}
