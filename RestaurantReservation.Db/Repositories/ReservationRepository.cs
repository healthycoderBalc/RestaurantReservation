﻿using Microsoft.EntityFrameworkCore;
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
                    .Include(r => r.Orders)
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

        public void DeleteReservationAsync(Reservation reservation)
        {
            _dbContext.Reservations.Remove(reservation);
        }

        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            List<Reservation> reservations = await _dbContext.Reservations
                .Where(r => r.CustomerId == customerId)
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .Include(r => r.Restaurant)
                .ToListAsync();

            return reservations;
        }

        public async Task<List<Order>> ListOrderAndMenuItemsAsync(int reservationId)
        {
            List<Order> orders = await _dbContext.Orders
                .Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            return orders;
        }

        public async Task<List<MenuItem>> ListOrderedMenuItemsAsync(int reservationId)
        {
            List<MenuItem> menuItems = await _dbContext.OrderItems
                .Where(oi => oi.Order.ReservationId == reservationId)
                .Select(oi => oi.MenuItem)
                .ToListAsync();

            return menuItems;
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

        public async Task<bool> CustomerExistAsync(int customerId)
        {
            var customer = await _dbContext.Customers
                .AnyAsync(r => r.CustomerId == customerId);
            return customer;
        }

        public async Task<bool> RestaurantExistAsync(int restaurantId)
        {
            var restaurant = await _dbContext.Restaurants
                .AnyAsync(r => r.RestaurantId == restaurantId);
            return restaurant;
        }

        public async Task<bool> TableExistAsync(int tableId)
        {
            var table = await _dbContext.Tables
                .AnyAsync(r => r.TableId == tableId);
            return table;
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
