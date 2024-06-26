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
        public async Task Demonstrate()
        {
            using (var tableDemonstration = new TableRepository())
            {
                Console.WriteLine("----------TABLE DEMONSTRATION---------");

                int restaurantId = 1;
                int capacity = 10;
                int createdTableId = await tableDemonstration.CreateTable(restaurantId, capacity);
                await tableDemonstration.ReadTable(createdTableId);

                int newRestaurantId = 2;
                int newCapacity = 6;
                await tableDemonstration.UpdateTable(createdTableId, newRestaurantId, newCapacity);
                await tableDemonstration.ReadTable(createdTableId);

                await tableDemonstration.DeleteTable(createdTableId);
                await tableDemonstration.ReadTable(createdTableId);
            }
        }
    }
}
