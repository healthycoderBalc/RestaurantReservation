using RestaurantReservation.Db.Models;
using RestaurantReservation.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class EmployeeFunctionalityDemonstration : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public EmployeeFunctionalityDemonstration()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public int CreateEmployee(int restaurantId, string firstName, string lastName, string position)
        {
            var restaurant = _dbContext.Restaurants.Find(restaurantId);
            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return 0;
            }

            var newEmployee = new Employee
            {
                Restaurant = restaurant,
                FirstName = firstName,
                LastName = lastName,
                Position = position
            };

            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
            return newEmployee.EmployeeId;
        }

        public void ReadEmployee(int employeeId)
        {
            var employee = _dbContext.Employees.Find(employeeId);

            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return;
            }

            Console.WriteLine($"Employee found: {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
        }

        public void UpdateEmployee(int employeeId, int restaurantId, string firstName, string lastName, string position)
        {
            var employee = _dbContext.Employees.Find(employeeId);
            var newRestaurant = _dbContext.Restaurants.Find(restaurantId);
            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return;
            }
            if (newRestaurant == null)
            {
                Console.WriteLine($"Restaurant with ID {restaurantId} not found.");
                return;
            }

            employee.Restaurant = newRestaurant;
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Position = position;

            _dbContext.SaveChanges();
            Console.WriteLine($"Employee {employeeId} updated successfully.");
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _dbContext.Employees.Find(employeeId);
            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return;
            }

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            Console.WriteLine($"Employee {employeeId} deleted successfully.");
        }
    }
}
