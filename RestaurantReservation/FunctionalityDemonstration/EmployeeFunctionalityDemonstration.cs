using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class EmployeeFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public void Demonstrate()
        {
            using (var employeeDemonstration = new EmployeeRepository())
            {

                int restaurantId = 1;
                string firstName = "Mary";
                string lastName = "Jane";
                string position = "Manager";
                var newEmployeeId = employeeDemonstration.CreateEmployee(restaurantId, firstName, lastName, position);
                employeeDemonstration.ReadEmployee(newEmployeeId);

                int newRestaurantId = 1;
                string newFirstName = "Michelle";
                string newLastName = "Jones";
                string newPosition = "Supervisor";
                employeeDemonstration.UpdateEmployee(newEmployeeId, newRestaurantId, newFirstName, newLastName, newPosition);
                employeeDemonstration.ReadEmployee(newEmployeeId);

                employeeDemonstration.DeleteEmployee(newEmployeeId);
                employeeDemonstration.ReadEmployee(newEmployeeId);
            }
        }
    }
}
