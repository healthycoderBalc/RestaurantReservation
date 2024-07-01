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
    public class EmployeeRepository : IDisposable, IEmployeeRepository
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public EmployeeRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _dbContext.Employees
                .Include(e => e.Restaurant)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeAsync(int employeeId, bool includeLists)
        {
            if (includeLists)
            {
                return await _dbContext.Employees
                    .Include(e => e.Restaurant)
                    .Include(e => e.Orders)
                    .Where(e => e.EmployeeId == employeeId)
                    .FirstOrDefaultAsync();
            }
            return await _dbContext.Employees
                    .Include(e => e.Restaurant)
                    .Where(e => e.EmployeeId == employeeId)
                    .FirstOrDefaultAsync();
        }


        public async Task CreateEmployeeAsync(int restaurantId, Employee employee)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                restaurant.Employees.Add(employee);
            }
        }

        public async Task ReadEmployeeAsync(int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);

            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
                return;
            }

            Console.WriteLine($"Employee found: ID {employee.EmployeeId} - {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
        }

        public async Task UpdateEmployeeAsync(int employeeId, int restaurantId, string firstName, string lastName, string position)
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

        public void DeleteEmployeeAsync(Employee employee)
        {

            _dbContext.Employees.Remove(employee);

        }

        public async Task<List<Employee>> ListManagersAsync()
        {
            var managerPosition = "Manager";
            List<Employee> managers = await _dbContext.Employees
                .Where(e => e.Position == managerPosition)
                .Include(e => e.Restaurant)
                .ToListAsync();

            return managers;
        }

        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            List<Order> employeeOrders = await _dbContext.Orders
                .Where(o => o.EmployeeId == employeeId)
                .ToListAsync();

            if (employeeOrders.Count == 0) return 0;
            decimal average = employeeOrders
                .Average(o => o.TotalAmount);

            return average;
        }

        public async Task<List<EmployeeWithRestaurantDetails>> GetEmployeesWithRestaurantDetailsFromViewAsync()
        {
            List<EmployeeWithRestaurantDetails> employees = await _dbContext.EmployeesWithRestaurantDetails
                .ToListAsync();
            return employees;
        }

        public async Task<Employee?> ValidateUserCredentials(string? firstName, string? lastName, int id)
        {
            // assuming password is employeeId and username is combination of firstName and lastName
            var user = await _dbContext.Employees
                .Where(e => e.FirstName == firstName && e.LastName == lastName && e.EmployeeId == id)
                .FirstOrDefaultAsync();

            return user;
        }


        public async Task<bool> RestaurantExistsAsync(int restaurantId)
        {
            return await _dbContext.Restaurants.AnyAsync(r => r.RestaurantId == restaurantId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }

    }
}
