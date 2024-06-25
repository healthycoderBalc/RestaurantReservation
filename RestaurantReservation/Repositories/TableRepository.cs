using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Repositories
{
    public class TableRepository : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public TableRepository()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public async Task<int> CreateTable(int restaurantId, int capacity)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return 0;
            }

            var newTable = new Db.Models.Table
            {
                Restaurant = restaurant,
                Capacity = capacity
            };

            _dbContext.Tables.Add(newTable);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Table created with ID: {newTable.TableId}");

            return newTable.TableId;
        }

        public async Task ReadTable(int tableId)
        {
            var table = await _dbContext.Tables.FindAsync(tableId);

            if (table == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return;
            }
            Console.WriteLine($"Table found: ID {table.TableId} - In Restaurant: {table.Restaurant.Name}, Capacity: {table.Capacity}");
        }

        public async Task UpdateTable(int tableId, int restaurantId, int capacity)
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

        public async Task DeleteTable(int tableId)
        {
            var table = await _dbContext.Tables.FindAsync(tableId);
            if (table == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return;
            }
            _dbContext.Remove(table);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Table {tableId} deleted successfully.");
        }
    }
}
