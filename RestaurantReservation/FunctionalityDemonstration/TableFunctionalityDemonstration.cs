using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class TableFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public void Demonstrate()
        {
            using (var tableDemonstration = new TableRepository())
            {
                int restaurantId = 1;
                int capacity = 10;
                int createdTableId = tableDemonstration.CreateTable(restaurantId, capacity);
                tableDemonstration.ReadTable(createdTableId);

                int newRestaurantId = 2;
                int newCapacity = 6;
                tableDemonstration.UpdateTable(createdTableId, newRestaurantId, newCapacity);
                tableDemonstration.ReadTable(createdTableId);

                tableDemonstration.DeleteTable(createdTableId);
                tableDemonstration.ReadTable(createdTableId);
            }
        }
    }
}
