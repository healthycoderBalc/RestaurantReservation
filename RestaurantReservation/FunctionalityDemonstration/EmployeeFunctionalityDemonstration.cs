using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class EmployeeFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task Demonstrate()
        {
            using (var employeeDemonstration = new EmployeeRepository())
            {
                Console.WriteLine("----------EMPLOYEE DEMONSTRATION---------");

                int restaurantId = 1;
                string firstName = "Mary";
                string lastName = "Jane";
                string position = "Manager";
                var newEmployeeId = await employeeDemonstration.CreateEmployee(restaurantId, firstName, lastName, position);
                await employeeDemonstration.ReadEmployee(newEmployeeId);

                int newRestaurantId = 1;
                string newFirstName = "Michelle";
                string newLastName = "Jones";
                string newPosition = "Supervisor";
                await employeeDemonstration.UpdateEmployee(newEmployeeId, newRestaurantId, newFirstName, newLastName, newPosition);
                await employeeDemonstration.ReadEmployee(newEmployeeId);

                await employeeDemonstration.DeleteEmployee(newEmployeeId);
                await employeeDemonstration.ReadEmployee(newEmployeeId);

                List<Employee> managers = await employeeDemonstration.ListManagers();
                Console.WriteLine();
                Console.WriteLine("Managers: ");
                if (managers.Count > 0)
                {
                    foreach (var manager in managers)
                    {
                        Console.WriteLine($"{manager.EmployeeId}. {manager.FirstName} {manager.LastName}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Get employees with restaurant details (from view)");
                List<EmployeeWithRestaurantDetails> employeesFromView = await employeeDemonstration.GetEmployeesWithRestaurantDetailsFromView();
                if (employeesFromView.Count > 0)
                {
                    foreach (EmployeeWithRestaurantDetails employee in employeesFromView)
                    {
                        Console.WriteLine($"{employee.EmployeeId}. {employee.FirstName} {employee.LastName} Position: {employee.Position}| Restaurant: {employee.RestaurantId}. {employee.RestaurantName}, Address: {employee.Address}, Phone number: {employee.PhoneNumber}, Opening Hours: {employee.OpeningHours}");
                    }
                }
                Console.WriteLine();

            }
        }
    }
}
