using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationRepository : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public ReservationRepository()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public async Task<int> CreateReservationAsync(int customerId, int restaurantId, int tableId, DateTime reservationDate, int partySize)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return 0;
            }
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);

            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return 0;
            }
            var table = await _dbContext.Tables.FindAsync(tableId);

            if (table == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return 0;
            }
            var newReservation = new Reservation
            {
                Customer = customer,
                Restaurant = restaurant,
                Table = table,
                ReservationDate = reservationDate,
                PartySize = partySize
            };

            _dbContext.Reservations.Add(newReservation);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Reservation created with ID: {newReservation.ReservationId}");

            return newReservation.ReservationId;
        }


        public async Task ReadReservationAsync(int reservationId)
        {
            var reservation = await _dbContext.Reservations.FindAsync(reservationId);

            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return;
            }
            Console.WriteLine($"Reservation found: ID {reservation.ReservationId} - Customer: {reservation.Customer.FirstName} {reservation.Customer.FirstName}, Restaurant: {reservation.Restaurant.Name}, Table: {reservation.Table.TableId}, Date: {reservation.ReservationDate}, Size: {reservation.PartySize}");
        }

        public async Task UpdateReservationAsync(int reservationId, int customerId, int restaurantId, int tableId, DateTime reservationDate, int partySize)
        {
            var reservation = await _dbContext.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return;
            }
            var newCustomer = await _dbContext.Customers.FindAsync(customerId);
            if (newCustomer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return;
            }
            var newRestaurant = await _dbContext.Restaurants.FindAsync(restaurantId);

            if (newRestaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            var newTable = await _dbContext.Tables.FindAsync(tableId);

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

            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Reservation {reservationId} updated successfully.");
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _dbContext.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {reservationId} not found.");
                return;
            }
            _dbContext.Remove(reservation);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Reservation {reservationId} deleted successfully.");
        }

        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            List<Reservation> reservations = await _dbContext.Reservations
                .Where(r => r.CustomerId == customerId)
                .Include(r => r.Table)
                .Include(r => r.Restaurant)
                .ToListAsync();

            return reservations;
        }

        public async Task<List<ReservationWithDetails>> GetReservationsWithCustomerAndRestaurantInformationFromViewAsync()
        {
            List<ReservationWithDetails> reservations = await _dbContext.ReservationsWithDetails
                .ToListAsync();
            return reservations;
        }
    }
}
