using RestaurantReservation.Db.Models;
using RestaurantReservation.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Repositories
{
    public class EmployeeRepository : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public EmployeeRepository()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> CreateEmployee(int restaurantId, string firstName, string lastName, string position)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
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
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Employee created with ID: {newEmployee.EmployeeId}");
            return newEmployee.EmployeeId;
        }

        public async Task ReadEmployee(int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);

            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return;
            }

            Console.WriteLine($"Employee found: ID {employee.EmployeeId} - {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
        }

        public async Task UpdateEmployee(int employeeId, int restaurantId, string firstName, string lastName, string position)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            var newRestaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
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

            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Employee {employeeId} updated successfully.");
        }

        public async Task DeleteEmployee(int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return;
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Employee {employeeId} deleted successfully.");
        }

        public async Task<List<Employee>> ListManagers()
        {
            var managerPosition = "Manager";
            List<Employee> managers = await _dbContext.Employees
                .Where(e => e.Position == managerPosition)
                .ToListAsync();

            return managers;
        }

        public async Task<List<EmployeeWithRestaurantDetails>> GetEmployeesWithRestaurantDetailsFromView()
        {
            List<EmployeeWithRestaurantDetails> employees = await _dbContext.EmployeesWithRestaurantDetails
                .ToListAsync();
            return employees;
        }

    }
}
