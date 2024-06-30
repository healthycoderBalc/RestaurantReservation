using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class TableRepository : IDisposable, ITableRepository
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public TableRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Models.Table>> GetTablesAsync()
        {
            return await _dbContext.Tables
                .Include(t => t.Restaurant)
                .ToListAsync();
        }

        public async Task<Models.Table?> GetTableAsync(int tableId, bool includeLists)
        {
            if (includeLists)
            {
                return await _dbContext.Tables
                    .Include(t => t.Reservations)
                    .Include(t => t.Restaurant)
                    .Where(t => t.TableId == tableId)
                    .FirstOrDefaultAsync();
            }
            return await _dbContext.Tables
                    .Include(t => t.Restaurant)
                    .Where(t => t.TableId == tableId)
                    .FirstOrDefaultAsync();
        }


        public async Task CreateTableAsync(int restaurantId, Db.Models.Table table)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                _dbContext.Tables.Add(table);

            }
        }

        public async Task ReadTableAsync(int tableId)
        {
            var table = await _dbContext.Tables.FindAsync(tableId);

            if (table == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return;
            }
            Console.WriteLine($"Table found: ID {table.TableId} - In Restaurant: {table.Restaurant.Name}, Capacity: {table.Capacity}");
        }

        public async Task UpdateTableAsync(int tableId, int restaurantId, int capacity)
        {
            var table = await _dbContext.Tables.FindAsync(tableId);
            var newRestaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (table == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return;
            }
            if (newRestaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }
            table.Restaurant = newRestaurant;
            table.Capacity = capacity;

            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Table {tableId} updated successfully.");
        }

        public void DeleteTableAsync(Db.Models.Table table)
        {
            _dbContext.Tables.Remove(table);
        }

        public async Task<bool> RestaurantExistsAsync(int restaurantId)
        {
            var restaurant = await _dbContext.Restaurants
                .AnyAsync(r => r.RestaurantId == restaurantId);
            return restaurant;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
