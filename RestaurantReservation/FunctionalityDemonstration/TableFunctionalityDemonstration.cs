using RestaurantReservation.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class TableFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task DemonstrateAsync()
        {
            using (var tableDemonstration = new TableRepository())
            {
                Console.WriteLine("----------TABLE DEMONSTRATION---------");

                int restaurantId = 1;
                int capacity = 10;
                int createdTableId = await tableDemonstration.CreateTableAsync(restaurantId, capacity);
                await tableDemonstration.ReadTableAsync(createdTableId);

                int newRestaurantId = 2;
                int newCapacity = 6;
                await tableDemonstration.UpdateTableAsync(createdTableId, newRestaurantId, newCapacity);
                await tableDemonstration.ReadTableAsync(createdTableId);

                await tableDemonstration.DeleteTableAsync(createdTableId);
                await tableDemonstration.ReadTableAsync(createdTableId);
            }
        }
    }
}
