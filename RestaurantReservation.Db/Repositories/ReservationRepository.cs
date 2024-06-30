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
    public class ReservationRepository : IDisposable, IReservationRepository
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

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _dbContext.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Restaurant)
                .Include(r => r.Table)
                .ToListAsync();
        }

        public async Task<Reservation?> GetReservationAsync(int reservationId, bool includeLists)
        {
            if (includeLists)
            {
                return await _dbContext.Reservations
                    .Include(r => r.Customer)
                    .Include(r => r.Restaurant)
                    .Include(r => r.Table)
                    .Include(c => c.Orders)
                    .ThenInclude(o => o.Employee)
                    .Where(c => c.ReservationId == reservationId)
                    .FirstOrDefaultAsync();
            }
            return await _dbContext.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Restaurant)
                .Include(r => r.Table)
                .Where(c => c.ReservationId == reservationId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateReservationAsync(int customerId, int restaurantId, int tableId, Reservation reservation)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            var table = await _dbContext.Tables.FindAsync(tableId);

            if (customer != null && restaurant != null && table != null)
            {
                _dbContext.Reservations.Add(reservation);
            }
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

        public void DeleteReservationAsync(Reservation reservation)
        {
            _dbContext.Reservations.Remove(reservation);
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

        public async Task<bool> CustomerRestaurantAndTableExistsAsync(int customerId, int restaurantId, int tableId)
        {
            var customer = await _dbContext.Customers
                .AnyAsync(r => r.CustomerId == customerId);
            var restaurant = await _dbContext.Restaurants
                .AnyAsync(e => e.RestaurantId == restaurantId);
            var table = await _dbContext.Tables
                .AnyAsync(e => e.TableId == tableId);
            return customer && restaurant && table;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
