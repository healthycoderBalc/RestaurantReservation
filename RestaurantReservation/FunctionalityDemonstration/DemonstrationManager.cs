using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class DemonstrationManager
    {
        private readonly List<IFunctionalityDemonstration> _functionalityDemonstrations;

        public DemonstrationManager()
        {
            _functionalityDemonstrations = new List<IFunctionalityDemonstration>
            {
                new CustomerFunctionalityDemonstration(),
                new RestaurantFunctionalityDemonstration(),
                new TableFunctionalityDemonstration(),
                new EmployeeFunctionalityDemonstration(),
                new MenuItemFunctionalityDemonstration(),
                new ReservationFunctionalityDemonstration(),
                new OrderFunctionalityDemonstration(),
                new OrderItemFunctionalityDemonstration(),
            };
        }

        public void RunAllDemonstrations()
        {
            foreach (var demo in _functionalityDemonstrations)
            {
                demo.Demonstrate();
                Console.WriteLine();
            }
        }
    }
}
