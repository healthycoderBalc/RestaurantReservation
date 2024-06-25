using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class ReservationFunctionalityDemonstration : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public ReservationFunctionalityDemonstration()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public int CreateReservation(int customerId, int restaurantId, int tableId, DateTime reservationDate, int partySize)
        {
            var customer = _dbContext.Customers.Find(customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return 0;
            }
            var restaurant = _dbContext.Restaurants.Find(restaurantId);

            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return 0;
            }
            var table = _dbContext.Tables.Find(tableId);

            if (table == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return 0;
            }
            var newReservation = new Db.Models.Reservation
            {
                Customer = customer,
                Restaurant = restaurant,
                Table = table,
                ReservationDate = reservationDate,
                PartySize = partySize
            };

            _dbContext.Reservations.Add(newReservation);
            _dbContext.SaveChanges();
            return newReservation.ReservationId;
        }



        public void ReadReservation(int reservationId)
        {
            var reservation = _dbContext.Reservations.Find(reservationId);

            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return;
            }
            Console.WriteLine($"Reservation found: Customer: {reservation.Customer.FirstName} {reservation.Customer.FirstName}, Restaurant: {reservation.Restaurant.Name}, Table: {reservation.Table.TableId}, Date: {reservation.ReservationDate}, Size: {reservation.PartySize}");
        }

        public void UpdateReservation(int reservationId, int customerId, int restaurantId, int tableId, DateTime reservationDate, int partySize)
        {
            var reservation = _dbContext.Reservations.Find(reservationId);
            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return;
            }
            var newCustomer = _dbContext.Customers.Find(customerId);
            if (newCustomer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return;
            }
            var newRestaurant = _dbContext.Restaurants.Find(restaurantId);

            if (newRestaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            var newTable = _dbContext.Tables.Find(tableId);

            if (newTable == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return;
            }
            reservation.Customer = newCustomer;
            reservation.Restaurant = newRestaurant;
            reservation.Table = newTable;
            reservation.ReservationDate = reservationDate;
            reservation.PartySize = partySize;

            _dbContext.SaveChanges();
            Console.WriteLine($"Reservation {reservationId} updated successfully.");
        }

        public void DeleteReservation(int reservationId)
        {
            var reservation = _dbContext.Reservations.Find(reservationId);
            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return;
            }
            _dbContext.Remove(reservation);
            _dbContext.SaveChanges();
            Console.WriteLine($"Reservation {reservationId} deleted successfully.");
        }
    }
}
