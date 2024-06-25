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
    public class TableFunctionalityDemonstration : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public TableFunctionalityDemonstration()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public int CreateTable(int restaurantId, int capacity)
        {
            var restaurant = _dbContext.Restaurants.Find(restaurantId);
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
            _dbContext.SaveChanges();
            return newTable.TableId;
        }



        public void ReadTable(int tableId)
        {
            var table = _dbContext.Tables.Find(tableId);

            if (table == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return;
            }
            Console.WriteLine($"Table found: In Restaurant: {table.Restaurant.Name}, Capacity: {table.Capacity}");
        }

        public void UpdateTable(int tableId, int restaurantId, int capacity)
        {
            var table = _dbContext.Tables.Find(tableId);
            var newRestaurant = _dbContext.Restaurants.Find(restaurantId);
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

            _dbContext.SaveChanges();
            Console.WriteLine($"Table {tableId} updated successfully.");
        }

        public void DeleteTable(int tableId)
        {
            var table = _dbContext.Tables.Find(tableId);
            if (table == null)
            {
                Console.WriteLine($"Table with ID {tableId} not found.");
                return;
            }
            _dbContext.Remove(table);
            _dbContext.SaveChanges();
            Console.WriteLine($"Table {tableId} deleted successfully.");
        }
    }
}
